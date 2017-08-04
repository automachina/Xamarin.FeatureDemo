using System;
using System.Threading.Tasks;
using FeatureDemo.Core.Models;
using Newtonsoft.Json;
using RestEase;
using Octokit;
using System.Collections.Generic;

//https://github.com/canton7/RestEase
namespace FeatureDemo.Core.Services
{
	[Header("User-Agent", "FeatureDemo")]
	public interface IGitHubApi
	{
		[Get("users/{userId}")]
		Task<User> GetUserAsync([Path] string userId);

        [Get("search/repositories{paramiters}")]
        Task<SearchRepositoryResult> SearchRepository([Body(BodySerializationMethod.UrlEncoded)] IDictionary<string, string> paramiters);
	}
}
