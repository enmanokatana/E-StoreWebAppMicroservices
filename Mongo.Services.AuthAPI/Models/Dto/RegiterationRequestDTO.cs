﻿namespace Mongo.Services.AuthAPI.Models.Dto;

public class RegiterationRequestDto
{
    public string Email { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public string? Rolename { get; set; }

}