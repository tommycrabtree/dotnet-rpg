using AutoMapper;
using dotnet_rpg.Data;
using dotnet_rpg.Dtos;
using dotnet_rpg.Dtos.CharacterDtos;
using dotnet_rpg.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public CharacterService(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;   
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var addCharacter = _mapper.Map<Character>(newCharacter);
            
            _context.Characters.Add(addCharacter);
            await _context.SaveChangesAsync();

            serviceResponse.Data = await _context.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();

            try
            {
                var delCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id)
                    ?? throw new Exception($"Character with Id '{id}' not found.");
                
                _context.Characters.Remove(delCharacter);

                await _context.SaveChangesAsync();

                serviceResponse.Data = await _context.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var dbCharacters = await _context.Characters.ToListAsync();
            serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            var dbCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacter);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();

            try
            {
                var updateCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id)
                    ?? throw new Exception($"Character with Id '{updatedCharacter.Id}' not found.");
                updateCharacter.Name = updatedCharacter.Name;
                updateCharacter.HitPoints = updatedCharacter.HitPoints;
                updateCharacter.Strength = updatedCharacter.Strength;
                updateCharacter.Defense = updatedCharacter.Defense;
                updateCharacter.Intelligence = updatedCharacter.Intelligence;
                updateCharacter.Archetype = updatedCharacter.Archetype;

                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetCharacterDto>(updateCharacter);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
    }
}
