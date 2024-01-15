﻿using BlogApp.Models.Enum;

namespace BlogApp.Api.Models;

public class UserModel
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string ProfilePictureName { get; set; }
    public RoleEnum Role {  get; set; }
}