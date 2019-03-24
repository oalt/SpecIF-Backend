﻿/*
 * Copyright (c) MDD4All.de, Dr. Oliver Alt
 */
using MDD4All.SpecIF.DataModels.BaseTypes;
using MDD4All.SpecIF.DataModels.Converters;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace MDD4All.SpecIF.DataModels
{
	[JsonConverter(typeof(KeyConverter))]
	public class Key : SpecIfElement
	{
		[JsonProperty(PropertyName = "id")]
		[BsonElement("id")]
		public string ID { get; set; }

		[JsonProperty(PropertyName = "revision")]
		[BsonElement("revision")]
		public int Revision { get; set; }
	}
}