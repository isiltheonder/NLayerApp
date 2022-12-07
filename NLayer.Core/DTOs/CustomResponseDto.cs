using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NLayer.Core.DTO_s
{
    public class CustomResponseDto<T>
    {
        //Endpointlerden geriye tek bir model dönün. Endpointte İşlem başarılı da başarısız da olsa geriye tek bir model dönün. Bu mode (class) içerisinde başarılıysa geriye data, başarısızsa data dönün. Tek bir model dönmek clientları rahatlatacaktır.

        //Static Factory Methodlar'da ayrı interface ve classlar oluşturmaya gerek olmaz, direkt olarak sınıf içerisinde nesne örnekleri dönebiliriz ve nesne oluşturma işlemini kontrol altına alabiliriz.

        public T Data { get; set; }

        [JsonIgnore]
        public int StatusCode { get; set; }

        public List<String> Errors { get; set; }

        public static CustomResponseDto<T> Success(int statusCode, T data)
        {
            return new CustomResponseDto<T> { Data = data, StatusCode = statusCode};

        }

        public static CustomResponseDto<T> Success(int statusCode)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode};

        }

        public static CustomResponseDto<T> Fail(int statusCode, List<string> errors)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode, Errors=errors };

        }

        //tek bir hata gelmesi durumuna karşı yazılan metod.
        public static CustomResponseDto<T> Fail(int statusCode, string error)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode, Errors = new List<string> { error } };

        }
    }
}
