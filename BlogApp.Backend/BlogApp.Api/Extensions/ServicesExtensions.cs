using BlogApp.Core;
using BlogApp.Core.Intefaces;
using BlogApp.Repository;
using BlogApp.Repository.Interfaces;
using BlogApp.Repository.SqlRepository;

namespace BlogApp.Api.Extensions;

public static class ServicesExtensions
{
    public static void AddRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IConnectionFactory, ConnectionFactory>();
        serviceCollection.AddSingleton<IQueryExecutor, SqlQueryExecutor>();
        serviceCollection.AddSingleton<IUsersRepository, UsersRepository>();
        serviceCollection.AddSingleton<IPostsRepository, PostsRepository>();
        serviceCollection.AddSingleton<IPostsReviewsRepository, PostsReviewsRepository>();
        serviceCollection.AddSingleton<IPostsLikesRepository, PostsLikesRepository>();
        serviceCollection.AddSingleton<IPostsCommentsRepository, PostsCommentsRepository>();
        serviceCollection.AddSingleton<ISavedPostsRepository, SavedPostsRepository>();
    }

    public static void AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IUsersService, UsersService>();
        serviceCollection.AddSingleton<IPostsService, PostsService>();
        serviceCollection.AddSingleton<IPostsReviewsService, PostsReviewsService>();
        serviceCollection.AddSingleton<IPostsLikesService, PostsLikesService>();
        serviceCollection.AddSingleton<IPostsCommentsService, PostsCommentsService>();
        serviceCollection.AddSingleton<ISavedPostsService, SavedPostsService>();
        serviceCollection.AddSingleton<IImageService, ImageService>();
        serviceCollection.AddSingleton<IPasswordService, PasswordService>();
        serviceCollection.AddSingleton<IAuthenticationService, AuthenticationService>();
        serviceCollection.AddSingleton<ITokenService, TokenService>();
    }
}