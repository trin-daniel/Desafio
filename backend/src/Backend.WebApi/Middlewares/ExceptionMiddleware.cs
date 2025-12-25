using Backend.Application.Exceptions;
using Backend.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Backend.WebApi.Middlewares;

// A responsabilidade desse middleware é capturar e tratar quaisquer exceções lançadas pela aplicação.
public class ExceptionMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (NotFoundException exception)
        {
            var problemDetails = new ProblemDetails
            {
                Type = "https://datatracker.ietf.org/doc/html/rfc9110#name-404-not-found",
                Title = "Recurso não encontrado.",
                Detail = exception.Message,
                Status = StatusCodes.Status404NotFound,
                Instance = context.Request.Path
            };
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            context.Response.ContentType = "application/problem+json";
            await context.Response.WriteAsJsonAsync(problemDetails);
        }
        catch (RecursoJaExisteException exception)
        {
            var problemDetails = new ProblemDetails
            {
                Type = "https://datatracker.ietf.org/doc/html/rfc9110#name-422-unprocessable-content",
                Title = "Ocorreu um conflito.",
                Detail = exception.Message,
                Status = StatusCodes.Status409Conflict,
                Instance = context.Request.Path
            };
            context.Response.StatusCode = StatusCodes.Status409Conflict;
            context.Response.ContentType = "application/problem+json";
            await context.Response.WriteAsJsonAsync(problemDetails);
        }
        catch (TransacaoReceitaMenorDeIdadeNaoPermitidaException exception)
        {
            var problemDetails = new ProblemDetails
            {
                Type = "https://datatracker.ietf.org/doc/html/rfc9110#name-422-unprocessable-content",
                Title = "Violação de regra de negócio.",
                Detail = exception.Message,
                Status = StatusCodes.Status422UnprocessableEntity,
                Instance = context.Request.Path
            };
            context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
            context.Response.ContentType = "application/problem+json";
            await context.Response.WriteAsJsonAsync(problemDetails);
        }
        catch (ViolacaoTransacaoException exception)
        {
            var problemDetails = new ProblemDetails
            {
                Type = "https://datatracker.ietf.org/doc/html/rfc9110#name-422-unprocessable-content",
                Title = "Violação de regra de negócio.",
                Detail = exception.Message,
                Status = StatusCodes.Status422UnprocessableEntity,
                Instance = context.Request.Path
            };
            context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
            context.Response.ContentType = "application/problem+json";
            await context.Response.WriteAsJsonAsync(problemDetails);
        }
        catch (Exception)
        {
            var problemDetails = new ProblemDetails
            {
                Type = "https://datatracker.ietf.org/doc/html/rfc9110#name-500-internal-server-error",
                Title = "Ocorreu um erro",
                Detail = "Erro interno do servidor. Tente novamente mais tarde.",
                Status = StatusCodes.Status500InternalServerError,
                Instance = context.Request.Path
            };
            context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
            context.Response.ContentType = "application/problem+json";
            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }
}