using AutoMapper;
using GetPet.BusinessLogic;
using GetPet.BusinessLogic.Model;
using GetPet.BusinessLogic.Model.Filters;
using GetPet.BusinessLogic.Repositories;
using GetPet.Data.Entities;
using GetPet.WebApi.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GetPet.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticlesController : BaseController
    {
        private readonly ILogger<ArticlesController> _logger;
        private readonly IMapper _mapper;
        private readonly IArticleRepository _articleRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IUnitOfWork _unitOfWork;

        #region Ctor

        public ArticlesController(
            ILogger<ArticlesController> logger,
            IMapper mapper,
            IArticleRepository articleRepository,
            ICommentRepository commentRepository,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _mapper = mapper;
            _articleRepository = articleRepository;
            _commentRepository = commentRepository;
            _unitOfWork = unitOfWork;
        }

        #endregion

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] BaseFilter filter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var articles = await _articleRepository.SearchAsync(filter);

            return Ok(_mapper.Map<IEnumerable<ArticleDto>>(articles));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var Article = await _articleRepository.GetByIdAsync(id);
            if (Article == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ArticleDto>(Article));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(ArticleDto article)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var articleToInsert = _mapper.Map<Article>(article);

            await _articleRepository.AddAsync(articleToInsert);

            await _unitOfWork.SaveChangesAsync();

            return Ok(articleToInsert);
        }

        [Authorize]
        [ValidateModel]
        [HttpPost("{articleId}/comments")]
        public async Task<IActionResult> PostComment(int articleId, CommentDto comment)
        {                   
            var commentToInsert = _mapper.Map<Comment>(comment);
            commentToInsert.ArticleId = articleId;
            commentToInsert.UserId = CurrentUser.Id;

            await _commentRepository.AddAsync(commentToInsert);
            await _unitOfWork.SaveChangesAsync();

            var comments = await LoadComments(articleId);

            return Ok(comments);            
        }

        [HttpGet("{articleId}/comments")]
        public async Task<IActionResult> GetComments(int articleId)
        {
            var mappedComments = await LoadComments(articleId);

            return Ok(mappedComments);
        }

        private async Task<IEnumerable<CommentDto>> LoadComments(int articleId)
        {
            var comments = await _commentRepository.SearchAsync(new CommentFilter
            {
                ArticleId = articleId
            });
            var mappedComments = _mapper.Map<IEnumerable<CommentDto>>(comments);

            return mappedComments;
        }
    }
}