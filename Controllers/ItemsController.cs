using ApiResponseExamplesDemo.Models;
using Authentication.API.Swagger;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace ApiResponseExamplesDemo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemsController : ControllerBase
{
    private readonly IValidator<MyPayload> _validator;

    public ItemsController(IValidator<MyPayload> validator)
    {
        _validator = validator;
    }

    [HttpPut("{id:int}")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(SuccessEnvelope<MyDTO>), Description = "Sucesso com envelope padrão")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails), Description = "Erro de validação")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, Type = typeof(ProblemDetails), Description = "Não autorizado")]
    [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ProblemDetails), Description = "Recurso não encontrado")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails), Description = "Erro inesperado")]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SucessDetailsExample))]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ProblemDetailsBadRequestExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(ProblemDetailsUnauthorizedExample))]
    [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(ProblemDetailsNotFoundExample))]
    [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ProblemDetailsInternalServerErrorExample))]
    public async Task<IActionResult> Update(int id, [FromBody] MyPayload payload, [FromServices] IServiceProvider serviceProvider)
    {
        try
        {
            var validationResult = _validator.Validate(payload);

            if (!validationResult.IsValid)
            {
                if (!validationResult.IsValid)
                {
                    throw new InvalidOperationException("Payload inválido: " + string.Join("; ", validationResult.Errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}")));
                }
            }
            var existingItem = id == 999 ? null : new MyDTO { Id = id, Nome = "Nome Antigo", AtualizadoEm = DateTime.UtcNow.AddDays(-1) };

            if (existingItem == null)
            {
                var notFoundDetails = ProblemDetailsExampleFactory.ForNotFound("Item não encontrado", HttpContext.Request.Path);
                return NotFound(notFoundDetails);
            }

            existingItem.Nome = payload.Nome;
            existingItem.AtualizadoEm = DateTime.UtcNow;

            var successResponse = SuccessEnvelope<MyDTO>.Ok(existingItem, HttpContext.Request.Path);
            return Ok(successResponse);


        }
        catch (InvalidOperationException ex)
        {
            var problemDetails = ProblemDetailsExampleFactory.ForBadRequest(ex.Message, HttpContext.Request.Path);
            return BadRequest(problemDetails);
        }
        catch (UnauthorizedAccessException ex)
        {
            var problemDetails = ProblemDetailsExampleFactory.ForUnauthorized(ex.Message, HttpContext.Request.Path);
            return Unauthorized(problemDetails);
        }
        catch (Exception)
        {
            var problemDetails = ProblemDetailsExampleFactory.ForInternalServerError("Ocorreu um erro inesperado ao atualizar o item.", HttpContext.Request.Path);
            return StatusCode(StatusCodes.Status500InternalServerError, problemDetails);
        }
    }
}