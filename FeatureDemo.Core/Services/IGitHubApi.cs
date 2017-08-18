using System;
using System.Threading.Tasks;
using FeatureDemo.Core.Models;
using Newtonsoft.Json;
using Refit;
using Octokit;
using System.Collections.Generic;

//https://github.com/canton7/RestEase
namespace FeatureDemo.Core.Services
{
    [Headers("User-Agent: Feature Demo Client")]
    public interface IGitHubApi
	{
		[Get("users/{userId}")]
		Task<User> GetUserAsync( string userId);

        [Get("search/repositories{paramiters}")]
        Task<SearchRepositoryResult> SearchRepository([Body(BodySerializationMethod.UrlEncoded)] IDictionary<string, string> paramiters);
	}
}
