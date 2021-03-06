﻿using Minerva.UI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Minerva.UI.Services
{
    public class MetricsService : IMetricsService
    {
        public HttpClient Client { get; set; }
        public string BaseUrl { get; set; }

        public MetricsService()
        {
            BaseUrl = "http://23.253.76.48";
            Client = new HttpClient();
        }

        public async Task<List<TestRun>> GetTestRunsAsync()
        {
            var response = await Client.GetAsync($"{BaseUrl}/runs");
            var responseBody = await response.Content.ReadAsStringAsync();
            var testRuns = JsonConvert.DeserializeObject<List<TestRun>>(responseBody);
            return testRuns;
        }

        public async Task<List<Test>> GetTestsAsync()
        {
            var response = await Client.GetAsync($"{BaseUrl}/tests");
            var responseBody = await response.Content.ReadAsStringAsync();
            var tests = JsonConvert.DeserializeObject<TestList>(responseBody);
            return tests.Tests;
        }

        public async Task<List<TestRun>> GetRunsByBuildNameAsync(string name)
        {
            var response = await Client.GetAsync($"{BaseUrl}/runs?build_name={name}");
            var responseBody = await response.Content.ReadAsStringAsync();
            var testRuns = JsonConvert.DeserializeObject<List<TestRun>>(responseBody);
            return testRuns;
        }

        public async Task<List<TestResult>> GetTestResultsByRunId(string id)
        {
            var response = await Client.GetAsync($"{BaseUrl}/runs/{id}/tests");
            var responseBody = await response.Content.ReadAsStringAsync();
            var results = JsonConvert.DeserializeObject<List<TestResult>>(responseBody);
            return results;
        }

    }

}
