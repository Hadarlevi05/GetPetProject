﻿using AutoMapper;
using GetPet.BusinessLogic.Model;
using GetPet.Common;
using GetPet.Data;
using GetPet.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PetAdoption.BusinessLogic.Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GetPet.BusinessLogic.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<IEnumerable<User>> SearchAsync(UserFilter filter);
        Task<User> GetByEmailAsync(string email);
        Task<User> Register(UserDto user);
        Task<LoginResponseDto> Login(LoginDto loginUser);
    }

    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UserRepository(
            GetPetDbContext getPetDbContext,
            IUnitOfWork unitOfWork,
            IMapper mapper) : base(getPetDbContext)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public override IQueryable<User> LoadNavigationProperties(IQueryable<User> query)
        {
            return query
                .Include(u => u.Organization)
                .Include(u => u.City);
        }

        public new async Task DeleteAsync(int id)
        {
            await base.DeleteAsync(id);
        }

        public async Task<IEnumerable<User>> SearchAsync(UserFilter filter)
        {
            var query = base.SearchAsync(entities.AsQueryable(), filter);

            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                query = query.Where(u => u.Name.StartsWith(filter.Name));
            }

            if (!string.IsNullOrWhiteSpace(filter.Email))
            {
                query = query.Where(u => u.Email.StartsWith(filter.Email));
            }

            if (!string.IsNullOrWhiteSpace(filter.CityName))
            {
                query = query.Where(u => u.City.Name.StartsWith(filter.CityName));
            }

            if (filter.EmailSubscription != null)
            {
                if (filter.EmailSubscription.Value)
                {
                    query = query.Where(u => u.EmailSubscription);
                }
                else
                {
                    query = query.Where(u => !u.EmailSubscription);
                }
            }
            return await query.ToListAsync();
        }

        public new async Task<User> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public new async Task AddAsync(User obj)
        {
            await base.AddAsync(obj);
        }

        public new async Task UpdateAsync(User obj)
        {
            await base.UpdateAsync(obj);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await entities.SingleOrDefaultAsync(u => u.Email.Equals(email, System.StringComparison.OrdinalIgnoreCase));

        }

        public async Task<User> Register(UserDto registeredUser)
        {
            registeredUser.Email = registeredUser.Email.Trim().ToLower();

            var existEmailUser = await GetByEmail(registeredUser.Email);
            if (existEmailUser != null)
            {
                throw new Exception("Email already exist");
            }

            var user = new User
            {
                CityId = registeredUser.CityId,
                PasswordHash = SecurePasswordHasher.Hash(registeredUser.Password),
                Email = registeredUser.Email,
                Name = registeredUser.Name,
                EmailSubscription = registeredUser.EmailSubscription,
                CreationTimestamp = DateTime.UtcNow,
                UpdatedTimestamp = DateTime.UtcNow,
            };

            if (registeredUser.Organization != null)
            {
                user.Organization = new Organization
                {
                    Name = registeredUser.Organization.Name,
                    Email = registeredUser.Email,
                    CreationTimestamp = DateTime.UtcNow,
                    UpdatedTimestamp = DateTime.UtcNow
                };
            }
            entities.Add(user);

            await _unitOfWork.SaveChangesAsync();

            return user;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 
        }

        public async Task<LoginResponseDto> Login(LoginDto loginUser)
        {
            var user = await GetByEmail(loginUser.Email);

            if (user == null)
                throw new Exception("Email doesn't exist");

            if (!SecurePasswordHasher.Verify(loginUser.Password, user.PasswordHash))
                throw new Exception("Password incorrect");

            user.LastLoginDate = DateTime.UtcNow;

            await _unitOfWork.SaveChangesAsync();
            var token = GenerateJwtToken(user);
            return new LoginResponseDto
            {
                Token = token,
                User = _mapper.Map<UserDto>(user)
            };
        }

        private async Task<User> GetByEmail(string email)
        {
            email = email.ToLower().Trim();

            return await entities.SingleOrDefaultAsync(u => u.Email == email);
        }

        private string GenerateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Constants.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}