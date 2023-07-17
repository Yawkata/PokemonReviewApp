﻿using AutoMapper;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Dto.AuthenticationDTOs;
using PokemonReviewApp.Dto.RequestDTOs;
using PokemonReviewApp.Dto.ResponseDTOs;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<Pokemon, PokemonDto>();
            CreateMap<PokemonDto, Pokemon>();
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();
            CreateMap<Country, CountryDto>();
            CreateMap<CountryDto, Country>();
            CreateMap<Owner, OwnerDto>();
            CreateMap<OwnerDto, Owner>();
            CreateMap<OwnerDto, OwnerLoginDTO>();
            CreateMap<Owner, OwnerLoginDTO>();
            CreateMap<OwnerLoginDTO, OwnerDto>();
            CreateMap<Owner, OwnerRegisterDTO>();
            CreateMap<OwnerLoginDTO, Owner>();
            CreateMap<OwnerRegisterDTO, Owner>();
            CreateMap<Review, ReviewDto>();
            CreateMap<ReviewDto, Review>();
            CreateMap<Reviewer, ReviewerDto>();
            CreateMap<ReviewerDto, Reviewer>();
            CreateMap<CategoryRequest, Category>();
            CreateMap<Category, CategoryRequest>();
            CreateMap<CategoryResponse, Category>();
            CreateMap<Category, CategoryResponse> ();
        }
    }
}
