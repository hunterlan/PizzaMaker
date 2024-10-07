namespace PizzaMaker.Presentation.Models.Users;

public class User
{
    public int Id { get; set; }
    public string? Fullname { get; set; }
    public required string Phone { get; set; }
    public string? Email { get; set; }
    public required string Password { get; set; }
    public int RoleId { get; set; }
}