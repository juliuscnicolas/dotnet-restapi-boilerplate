using Microsoft.AspNetCore.Mvc;
using RestApiBoilerplate.Application.Abstractions;
using RestApiBoilerplate.Application.Contracts;

namespace RestApiBoilerplate.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class PeopleController : ControllerBase
{
    private readonly IPersonService _personService;

    public PeopleController(IPersonService personService)
    {
        _personService = personService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyCollection<PersonDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyCollection<PersonDto>>> GetAll(CancellationToken cancellationToken)
    {
        var people = await _personService.GetAllAsync(cancellationToken);
        return Ok(people);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(PersonDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PersonDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var person = await _personService.GetByIdAsync(id, cancellationToken);
        return person is null ? NotFound() : Ok(person);
    }

    [HttpPost]
    [ProducesResponseType(typeof(PersonDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PersonDto>> Create([FromBody] CreatePersonRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var createdPerson = await _personService.CreateAsync(request, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = createdPerson.Id }, createdPerson);
        }
        catch (ArgumentException ex)
        {
            return ValidationProblem(new ValidationProblemDetails
            {
                Title = "Validation failed.",
                Detail = ex.Message
            });
        }
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePersonRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var updated = await _personService.UpdateAsync(id, request, cancellationToken);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return ValidationProblem(new ValidationProblemDetails
            {
                Title = "Validation failed.",
                Detail = ex.Message
            });
        }
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await _personService.DeleteAsync(id, cancellationToken);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}