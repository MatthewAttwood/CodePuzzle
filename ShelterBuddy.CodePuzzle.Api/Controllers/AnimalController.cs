using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using AutoMapper;
using ShelterBuddy.CodePuzzle.Api.Models;
using ShelterBuddy.CodePuzzle.Core.DataAccess;
using ShelterBuddy.CodePuzzle.Core.Entities;

namespace ShelterBuddy.CodePuzzle.Api.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class AnimalController : ControllerBase
{
    private readonly IRepository<Animal, Guid> _repository;
    private readonly IMapper _mapper;

    public AnimalController(IRepository<Animal, Guid> animalRepository, IMapper mapper)
    {
        _repository = animalRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public AnimalModel[] Get() => _repository.GetAll().Select(animal => new AnimalModel
    {
        Id = $"{animal.Id}",
        Name = animal.Name,
        Colour = animal.Colour,
        MicrochipNumber = animal.MicrochipNumber,
        Species = animal.Species,
        DateOfBirth = animal.DateOfBirth,
        DateInShelter = animal.DateInShelter,
        DateLost = animal.DateLost,
        DateFound = animal.DateFound,
        AgeYears = animal.AgeYears,
        AgeMonths = animal.AgeMonths,
        AgeWeeks = animal.AgeWeeks,
        AgeText = animal.AgeText
    }).ToArray();

    [HttpPost]
    public async Task<ActionResult<Animal>> Post(AnimalModel newAnimal)
    {
        try 
        {
            // Custom validation required
            // DateOfBirth or Age fields must be entered
            if (string.IsNullOrEmpty(Convert.ToString(newAnimal.DateOfBirth)) && string.IsNullOrEmpty(newAnimal.AgeText))
            {
                var validationError = "You must provide either the Date of Birth, or Age fields value(s).";
                this.ModelState.AddModelError("Date of Birth or Age", validationError); 
                return BadRequest(validationError);
            }
            
            // Create a new Animal
            var animal = new Animal();

            // Mapper
            //animal = _mapper.Map(Animal(newAnimal));


            _repository.Add(animal);

            //await _repository.SaveChangesAsync();

            return CreatedAtRoute("Test Value", "");
        }
        catch (Exception ex) 
        {
            return BadRequest(ex.Message);
        }
    }
}