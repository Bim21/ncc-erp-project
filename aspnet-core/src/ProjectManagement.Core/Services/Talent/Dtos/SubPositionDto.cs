﻿using Abp.Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Services.Talent.Dtos
{
    public class SubPositionDto
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("colorCode")]
        public string ColorCode { get; set; }
    }
}