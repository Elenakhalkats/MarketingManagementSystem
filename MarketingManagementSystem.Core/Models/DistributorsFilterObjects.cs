namespace MarketingManagementSystem.Core.Models;

public class DistributorsFilterObjects
{
	public DistributorsFilterObjects(
		string? firstName, 
		string? lastName)
	{
		FirstName = firstName;
		LastName = lastName;
	}
	public string? FirstName { get; set; }
	public string? LastName { get; set; }
}
