using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace Authentication.API.Swagger
{

    public static class ProblemDetailsExampleFactory
    {
        public static ProblemDetails ForBadRequest(string detail, string instance) => new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "Requisição inválida",
            Detail = detail,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Instance = instance
        };

        public static ProblemDetails ForUnauthorized(string detail, string instance) => new ProblemDetails
        {
            Status = StatusCodes.Status401Unauthorized,
            Title = "Não autorizado",
            Detail = detail,
            Type = "https://tools.ietf.org/html/rfc7235#section-3.1",
            Instance = instance
        };

        public static ProblemDetails ForNotFound(string detail, string instance) => new ProblemDetails
        {
            Status = StatusCodes.Status404NotFound,
            Title = "Não encontrado",
            Detail = detail,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
            Instance = instance
        };

        public static ProblemDetails ForInternalServerError(string detail, string instance) => new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "Erro interno do servidor",
            Detail = detail,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
            Instance = instance
        };

        public static ProblemDetails ForConflict(string detail, string instance) => new ProblemDetails
        {
            Status = StatusCodes.Status409Conflict,
            Title = "Conflito",
            Detail = detail,
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.8",
            Instance = instance
        };
    }

    public class ProblemDetailsBadRequestExample : IExamplesProvider<ProblemDetails>
    {
        public ProblemDetails GetExamples() => ProblemDetailsExampleFactory.ForBadRequest(
            "Um ou mais erros de validação ocorreram.",
            "/exemplo/instancia");
    }

    public class ProblemDetailsUnauthorizedExample : IExamplesProvider<ProblemDetails>
    {
        public ProblemDetails GetExamples() => ProblemDetailsExampleFactory.ForUnauthorized(
            "Falha na autenticação: credenciais inválidas.",
            "/exemplo/instancia");
    }

    public class ProblemDetailsInternalServerErrorExample : IExamplesProvider<ProblemDetails>
    {
        public ProblemDetails GetExamples() => ProblemDetailsExampleFactory.ForInternalServerError(
            "Ocorreu um erro inesperado.",
            "/exemplo/instancia");
    }

    public class ProblemDetailsNotFoundExample : IExamplesProvider<ProblemDetails>
    {
        public ProblemDetails GetExamples() => ProblemDetailsExampleFactory.ForNotFound(
            "O recurso solicitado não foi encontrado.",
            "/exemplo/instancia");
    }

    public class ProblemDetailsConflictExample : IExamplesProvider<ProblemDetails>
    {
        public ProblemDetails GetExamples() => ProblemDetailsExampleFactory.ForConflict(
            "A requisição conflita com o estado atual do recurso.",
            "/exemplo/instancia");
    }
}
