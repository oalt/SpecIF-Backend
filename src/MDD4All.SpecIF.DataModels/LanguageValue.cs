﻿/*
 * Copyright (c) MDD4All.de, Dr. Oliver Alt
 */
using MDD4All.SpecIF.DataModels.BaseTypes;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace MDD4All.SpecIF.DataModels
{
	[JsonObject]
    public class LanguageValue : SpecIfElement
	{
		[JsonProperty(PropertyName = "text")]
		[BsonElement("text")]
		public string Text { get; set; }

		[JsonProperty(PropertyName = "language")]
		[BsonElement("language")]
		public string Language { get; set; }
	}
}