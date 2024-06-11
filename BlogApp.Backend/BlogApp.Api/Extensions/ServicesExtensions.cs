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
        serviceCollection.AddTransient<IConnectionFactory, ConnectionFactory>();
        serviceCollection.AddTransient<IQueryExecutor, SqlQueryExecutor>();
        serviceCollection.AddTransient<IUsersRepository, UsersRepository>();
        serviceCollection.AddTransient<IPostsRepository, PostsRepository>();
        serviceCollection.AddTransient<IPostsReviewsRepository, PostsReviewsRepository>();
        serviceCollection.AddTransient<IPostsLikesRepository, PostsLikesRepository>();
        serviceCollection.AddTransient<IPostsCommentsRepository, PostsCommentsRepository>();
        serviceCollection.AddTransient<ISavedPostsRepository, SavedPostsRepository>();
        serviceCollection.AddTransient<IPostsCategoriesRepository, PostsCategoriesRepository>();
    }

    public static void AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IUsersService, UsersService>();
        serviceCollection.AddTransient<IPostsService, PostsService>();
        serviceCollection.AddTransient<IPostsReviewsService, PostsReviewsService>();
        serviceCollection.AddTransient<IPostsLikesService, PostsLikesService>();
        serviceCollection.AddTransient<IPostsCommentsService, PostsCommentsService>();
        serviceCollection.AddTransient<ISavedPostsService, SavedPostsService>();
        serviceCollection.AddTransient<IImageService, ImageService>();
        serviceCollection.AddTransient<IPasswordService, PasswordService>();
        serviceCollection.AddTransient<IAuthenticationService, AuthenticationService>();
        serviceCollection.AddTransient<ITokenService, TokenService>();
        serviceCollection.AddTransient<IPostsCategoriesService, PostsCategoriesService>();
    }
}