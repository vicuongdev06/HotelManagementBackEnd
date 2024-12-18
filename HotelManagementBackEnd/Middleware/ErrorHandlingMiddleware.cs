using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace HotelManagementBackEnd.Middleware
{
	public class ErrorHandlingMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ErrorHandlingMiddleware> _logger;

		public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception ex)
			{
				await HandleExceptionAsync(context, ex);
			}
		}

		private async Task HandleExceptionAsync(HttpContext context, Exception ex)
		{
			_logger.LogError(ex, "Unhandled exception occurred.");

			var response = context.Response;
			response.ContentType = "application/json";

			HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
			string errorMessage = "An unexpected error occurred.";

			switch (ex)
			{
				case KeyNotFoundException:
					statusCode = HttpStatusCode.NotFound;
					errorMessage = "The requested resource was not found.";
					break;

				case UnauthorizedAccessException:
					statusCode = HttpStatusCode.Unauthorized;
					errorMessage = "Access is denied.";
					break;

				case ValidationException validationEx:
					statusCode = HttpStatusCode.BadRequest;
					errorMessage = validationEx.Message;
					break;

				default:
					break;
			}

			var errorResponse = new
			{
				success = false,
				statusCode = (int)statusCode,
				message = errorMessage,
				details = ex.InnerException?.Message
			};

			response.StatusCode = (int)statusCode;
			await response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
		}
	}
}
