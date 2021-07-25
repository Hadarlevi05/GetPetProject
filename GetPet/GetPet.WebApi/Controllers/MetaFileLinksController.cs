using AutoMapper;
using GetPet.BusinessLogic;
using GetPet.BusinessLogic.Model;
using GetPet.BusinessLogic.Repositories;
using GetPet.Common;
using GetPet.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GetPet.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetaFileLinksController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ILogger<MetaFileLinksController> _logger;
        private readonly IMetaFileLinkRepository _metaFileLinkRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MetaFileLinksController(
                ILogger<MetaFileLinksController> logger,
                IMapper mapper,
                IMetaFileLinkRepository metaFileLinkRepository,
                IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _mapper = mapper;
            _metaFileLinkRepository = metaFileLinkRepository;
            _unitOfWork = unitOfWork;
        }

        private async Task<string> UploadFile(IFormFile formFile)
        {
            if (formFile != null && formFile.Length > 0)
            {
                var extension = formFile.FileName.Split(".").Last();
                var fileName = $"{Guid.NewGuid()}.{extension}";                
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\upload-content", fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(fileStream);
                }
                return $"{Constants.WEBAPI_URL}/upload-content/{fileName}";
            }
            return null ;
        }

        [HttpPost]
        public async Task<IActionResult> Post(IFormFile formFile)
        {
            var uploadFilename = await UploadFile(formFile);

            if (string.IsNullOrWhiteSpace(uploadFilename))
                return BadRequest();

            var mfl = await _metaFileLinkRepository.AddAsync(new MetaFileLink
            {
                Path = uploadFilename,
                MimeType = formFile.ContentType,
                Size = formFile.Length
            });

            await _unitOfWork.SaveChangesAsync();

            return Ok(_mapper.Map<MetaFileLinkDto>(mfl));
        }
    }
}