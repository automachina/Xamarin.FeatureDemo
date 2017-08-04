using System;
using Prism.Commands;
using Prism.Navigation;
using Octokit;
using RestEase;
using FeatureDemo.Core.Services;
using Xamarin.Forms;
using System.Collections.Generic;
using FeatureDemo.Core.Helpers;
using System.Text.RegularExpressions;

namespace FeatureDemo.Core.ViewModels
{
    public class RepositorySearchPageViewModel : BaseViewModel
    {
        //IGitHubApi api;
        Nav nav;

        public DelegateCommand<string> SearchCommand { get; private set; }
        public DelegateCommand RefreshCommand { get; private set; }
        public DelegateCommand<string> OpenRepoCommand { get; private set; }

        string _searchTerm; public string SearchTerm
        {
            get => _searchTerm;
            set => SetProperty(ref _searchTerm, value);
        }

        SearchRepositoryResult _results; public SearchRepositoryResult Results
        {
            get => _results;
            set => SetProperty(ref _results, value);
        }

        bool _isRefreshing; public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }

        public RepositorySearchPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "GitHub Repo Search";
            SearchCommand = new DelegateCommand<string>(SearchGitHub);
            RefreshCommand = new DelegateCommand(Refresh);
            OpenRepoCommand = new DelegateCommand<string>(OpenRepo);
            nav = new Nav();
            SearchTerm = "Octokit";
        }

        private void Refresh()
        {
            SearchGitHub(SearchTerm);
            IsRefreshing = false;
        }

        private async void OpenRepo(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                var nameRegex = Regex.Match(url, @"^https://github.com/[^/]*/(?<name>[^/]*)$");
                var name = nameRegex.Success ? nameRegex.Groups["name"]?.ToString() : "GitHub.com";
                await _navigationService.NavigateAsync(nav.To.WebView($"url={url}&title={name}").Go);
            }
        }

        private async void SearchGitHub(string term)
        {
            if (!string.IsNullOrEmpty(term))
            {
                IsBusy = true;
                var github = new GitHubClient(new ProductHeaderValue("FeatureDemo"));
                var request = new SearchRepositoriesRequest(term) { SortField = RepoSearchSort.Stars, Order = SortDirection.Descending };
                Results = await github.Search.SearchRepo(request);
                IsBusy = false;
            }
        }
    }
}
