using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace API.Extension
{
    /// <summary>
    /// Object Extension
    /// </summary>
    public static class ObjectExtension
    {
        /// <summary>
        /// Get string content
        /// </summary>
        /// <param name="item">Cordinates</param>
        /// <param name="encoding">encoding</param>
        /// <param name="contentType">content type</param>
        /// <returns>Eclassification</returns>
        public static StringContent AsContent(this object item, Encoding encoding = null, string contentType = "application/json")
        {
            if (encoding == null) encoding = Encoding.Default;

            var content = item == null ? string.Empty : JsonSerializer.Serialize(item);
            return new StringContent(content, encoding, contentType);
        }

        /// <summary>
        /// Validate a Object
        /// </summary>
        /// <param name="viewModel">model</param>
        /// <returns>true if valid</returns>
        public static bool IsValid(this object viewModel)
        {
            var context = new ValidationContext(viewModel, null, null);
            var results = new List<ValidationResult>();
            return Validator.TryValidateObject(viewModel, context, results, true);
        }

        /// <summary>
        /// Validate a Object
        /// </summary>
        /// <param name="viewModel">model</param>
        /// <returns>List of validations</returns>
        public static IEnumerable<ValidationResult> Validate(this object viewModel)
        {
            var context = new ValidationContext(viewModel, null, null);
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(viewModel, context, results, true);

            return results;
        }
    }
}
