namespace tareaSem9.Core.Domain.Entities;

public partial class Usuario
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Passwordhash { get; set; } = null!;

    public string Role { get; set; } = null!;
}
