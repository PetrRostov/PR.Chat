namespace PR.Chat.Infrastructure.Data
{
    internal static class ExceptionHelper
    {
         internal static void EntityNotFound<T>(string paramName, string paramValue)
         {
             throw new EntityNotFoundException<T>(string.Format("{0} not found with {1}={2}", typeof(T).Name, paramName, paramValue));
         }
    }
}