using System;
using System.Threading.Tasks;
using FeatureDemo.Core.Models;
using Newtonsoft.Json;
using RestEase;

//https://github.com/canton7/RestEase
namespace FeatureDemo.Core.Services
{
	[Header("User-Agent", "FeatureDemo")]
	public interface IGitHubApi
	{
		[Get("users/{userId}")]
		Task<User> GetUserAsync([Path] string userId);
	}
}
