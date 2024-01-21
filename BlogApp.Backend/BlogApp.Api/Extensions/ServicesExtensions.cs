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
        serviceCollection.AddSingleton<IUserRepository, UserRepository>();
        serviceCollection.AddSingleton<IPostRepository, PostRepository>();
        serviceCollection.AddSingleton<IPostReviewRepository, PostReviewRepository>();
    }

    public static void AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IUserService, UserService>();
        serviceCollection.AddSingleton<IPostService, PostService>();
        serviceCollection.AddSingleton<IPostReviewService, PostReviewService>();
        serviceCollection.AddSingleton<IImageService, ImageService>();
        serviceCollection.AddSingleton<IPasswordService, PasswordService>();
    }
}