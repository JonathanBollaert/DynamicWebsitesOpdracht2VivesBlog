﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Vives.Services.Model;
using Vives.Services.Model.Extensions;
using VivesBlog.Dto.Requests;
using VivesBlog.Dto.Results;

namespace VivesBlog.Sdk
{
    public class ArticleSdk(IHttpClientFactory httpClientFactory)
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

        //Find
        public async Task<IList<ArticleResult>> Find()
        {
            var httpClient = CreateHttpClient();

            var route = "Articles";
            var response = await httpClient.GetAsync(route);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<IList<ArticleResult>>();
            if (result is null)
            {
                return new List<ArticleResult>();
            }

            return result;
        }

        //Get
        public async Task<ServiceResult<ArticleResult>> Get(int id)
        {
            var httpClient = CreateHttpClient();

            var route = $"Articles/{id}";
            var response = await httpClient.GetAsync(route);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ServiceResult<ArticleResult>>();
            if (result is null)
            {
                return new ServiceResult<ArticleResult>();
            }

            return result;
        }

        //Create
        public async Task<ServiceResult<ArticleResult>> Create(ArticleRequest request)
        {
            var httpClient = CreateHttpClient();

            var route = "Articles";
            var response = await httpClient.PostAsJsonAsync(route, request);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ServiceResult<ArticleResult>>();
            if (result is null)
            {
                return new ServiceResult<ArticleResult>();
            }

            return result;
        }

        //Update
        public async Task<ServiceResult<ArticleResult>> Update(int id, ArticleRequest request)
        {
            var httpClient = CreateHttpClient();

            var route = $"Articles/{id}";
            var response = await httpClient.PutAsJsonAsync(route, request);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ServiceResult<ArticleResult>>();
            if (result is null)
            {
                result = new ServiceResult<ArticleResult>();
                result.NotFound(nameof(ArticleResult), id);
            }

            return result;
        }

        //Delete
        public async Task<ServiceResult<ArticleResult>> Delete(int id)
        {
            var httpClient = CreateHttpClient();

            var route = $"Articles/{id}";
            var response = await httpClient.DeleteAsync(route);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ServiceResult<ArticleResult>>();
            if (result is null)
            {
                return new ServiceResult<ArticleResult>();
            }

            return result;
        }

        private HttpClient CreateHttpClient()
        {
            return _httpClientFactory.CreateClient("VivesBlogApi");
        }
    }
}
