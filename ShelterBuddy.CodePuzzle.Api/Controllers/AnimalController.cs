using Microsoft.AspNetCore.Mvc;
using ShelterBuddy.CodePuzzle.Api.Models;
using ShelterBuddy.CodePuzzle.Core.DataAccess;
using ShelterBuddy.CodePuzzle.Core.Entities;
using System.Net;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace ShelterBuddy.CodePuzzle.Api.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class AnimalController : ControllerBase
{
    private readonly IRepository<Animal, Guid> _repository;

    public AnimalController(IRepository<Animal, Guid> animalRepository)
    {
        _repository = animalRepository;
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
    public string Post(AnimalModel newAnimal)
    {
        // Custom validation required
        // DateOfBirth or Age fields must be entered
        if (string.IsNullOrEmpty(Convert.ToString(newAnimal.DateOfBirth)) && string.IsNullOrEmpty(newAnimal.AgeText))
        {
            var validationError = "You must provide either the Date of Birth, or Age fields value(s).";
            this.ModelState.AddModelError("Date of Birth or Age", validationError);
        }
        
        // Mapper (would use automapper in production)
        var animal = new Animal
        {
            Name = newAnimal.Name ?? null,
            Colour = newAnimal.Colour ?? null,
            MicrochipNumber = newAnimal.MicrochipNumber ?? null,
            Species = newAnimal.Species ?? null,
            DateOfBirth = newAnimal.DateOfBirth ?? null,
            DateInShelter = newAnimal.DateInShelter ?? null,
            DateFound = newAnimal.DateFound ?? null,
            DateLost = newAnimal.DateLost ?? null,
            AgeYears = newAnimal.AgeYears ?? null,
            AgeMonths = newAnimal.AgeMonths ?? null,
            AgeWeeks = newAnimal.AgeWeeks ?? null
        };

        _repository.Add(animal);
        _repository.Save();

        return "Animal added to data source successfully.";
    }
}