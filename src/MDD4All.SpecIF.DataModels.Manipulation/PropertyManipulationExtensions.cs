﻿/*
 * Copyright (c) MDD4All.de, Dr. Oliver Alt
 */
using MDD4All.SpecIF.DataProvider.Contracts;
using System.Linq;

namespace MDD4All.SpecIF.DataModels.Manipulation
{
    public static class PropertyManipulationExtensions
    {
		public static string GetDataTypeType(this Property property, ISpecIfMetadataReader dataProvider)
		{
			string result = "";

			PropertyClass propertyClass = dataProvider.GetPropertyClassByKey(property.PropertyClass);

			if (propertyClass != null)
			{
				DataType dataType = dataProvider.GetDataTypeById(propertyClass.DataType);
				result = dataType.Type;
			}
			return result;
		}

		public static DataType GetDataType(this Property property, ISpecIfMetadataReader dataProvider)
		{
			DataType result = null;

			PropertyClass propertyClass = dataProvider.GetPropertyClassByKey(property.PropertyClass);

			if (propertyClass != null)
			{
				DataType dataType = dataProvider.GetDataTypeById(propertyClass.DataType);
				result = dataType;
			}
			return result;
		}

		public static string GetStringValue(this Property property, ISpecIfMetadataReader dataProvider, string language = "de")
		{
			string result = "";

			if(property.GetDataTypeType(dataProvider) == "xs:enumeration")
			{
				PropertyClass propertyClass = dataProvider.GetPropertyClassByKey(property.PropertyClass);

				bool? isMultiple = null;

				if(propertyClass != null)
				{
					isMultiple = propertyClass.Multiple;
				}

				DataType enumDataType = property.GetDataType(dataProvider);

				if(enumDataType != null)
				{
					if (isMultiple != null && isMultiple == true)
					{
						if (property.Value.LanguageValues?.FirstOrDefault() != null)
						{
							char[] separator = { ',' };


							string[] values = property.Value.LanguageValues[0].Text.Split(separator);

							int counter = 0;

							foreach (string enumId in values)
							{
								EnumValue value = enumDataType.Values.Find(val => val.ID == enumId.Trim());

								if (value != null)
								{
									result += value.Value;

									if (counter < values.Length - 1)
									{
										result += ", ";
									}

								}
								counter++;
							}
						}

					}
					else
					{
						if (property.Value.LanguageValues?.FirstOrDefault() != null)
						{
							string enumId = property.Value.LanguageValues[0].Text;
							EnumValue value = enumDataType.Values.Find(val => val.ID == enumId);

							if (value != null)
							{
								result = value.Value.LanguageValues?.FirstOrDefault(val => val.Language == language).Text;
							}
						}

					}
				}
			}
			else
			{
				LanguageValue languageValue = property.Value.LanguageValues.FirstOrDefault(val => val.Language == language);
				
				if(languageValue == null)
				{
					languageValue = property.Value.LanguageValues?.FirstOrDefault();
				}

				if (languageValue != null)
				{
					result = languageValue.Text;
				}
			}
			return result;
		}
    }
}