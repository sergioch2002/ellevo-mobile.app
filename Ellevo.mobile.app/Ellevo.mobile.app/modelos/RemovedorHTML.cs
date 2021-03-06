﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ellevo.mobile.app.modelos
{
    public static class RemovedorHTML
    {
        /// <summary>
        /// Remove HTML from string with Regex.
        /// </summary>
        public static string RemoveTagsRegex(string source)
        {
            return Regex.Replace(source, "<.*?>", string.Empty);
        }

        
        /// <summary>
        /// Remove HTML tags from string using char array.
        /// </summary>
        public static string RemoveTagsCharArray(string source)
        {
            char[] array = new char[source.Length];
            int arrayIndex = 0;
            bool inside = false;

            for (int i = 0; i < source.Length; i++)
            {
                char let = source[i];
                if (let == '<')
                {
                    inside = true;
                    continue;
                }
                if (let == '>')
                {
                    inside = false;
                    continue;
                }
                if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }
            return new string(array, 0, arrayIndex);
        }
    }
}
