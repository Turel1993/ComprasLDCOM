#pragma warning disable 108 // Disable "CS0108 '{derivedDto}.ToJson()' hides inherited member '{dtoBase}.ToJson()'. Use the new keyword if hiding was intended."
#pragma warning disable 114 // Disable "CS0114 '{derivedDto}.RaisePropertyChanged(String)' hides inherited member 'dtoBase.RaisePropertyChanged(String)'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword."
#pragma warning disable 472 // Disable "CS0472 The result of the expression is always 'false' since a value of type 'Int32' is never equal to 'null' of type 'Int32?'
#pragma warning disable 1573 // Disable "CS1573 Parameter '...' has no matching param tag in the XML comment for ...
#pragma warning disable 1591 // Disable "CS1591 Missing XML comment for publicly visible type or member ..."

namespace ComprasLDCOM.API
{ 
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Windows.Markup;
    using System = global::System;
    using ComprasLDCOM.Datos.Inicio.Response;
    using ComprasLDCOM.Datos.Carrito.Response;
    using ComprasLDCOM.Datos.Cuenta.BaseDeDatos;
    using ComprasLDCOM.Datos.Cuenta.Response;

    public partial class API
    {
        //private string _baseUrl = "http://192.168.2.249";
        //private string _baseUrl = "http://187.188.102.33"; //publica aph
        private string _baseUrl;
        private System.Net.Http.HttpClient _httpClient;
        private System.Lazy<Newtonsoft.Json.JsonSerializerSettings> _settings;

        public API(System.Net.Http.HttpClient httpClient)
        {
            _httpClient = httpClient;
            _settings = new System.Lazy<Newtonsoft.Json.JsonSerializerSettings>(CreateSerializerSettings);
        }

        private Newtonsoft.Json.JsonSerializerSettings CreateSerializerSettings()
        {
            var settings = new Newtonsoft.Json.JsonSerializerSettings();
            UpdateJsonSerializerSettings(settings);
            return settings;
        }

        public string BaseUrl
        {
            get { return _baseUrl; }
            set { _baseUrl = value; }
        }

        protected Newtonsoft.Json.JsonSerializerSettings JsonSerializerSettings { get { return _settings.Value; } }

        partial void UpdateJsonSerializerSettings(Newtonsoft.Json.JsonSerializerSettings settings);
        partial void PrepareRequest(System.Net.Http.HttpClient client, System.Net.Http.HttpRequestMessage request, string url);
        partial void PrepareRequest(System.Net.Http.HttpClient client, System.Net.Http.HttpRequestMessage request, System.Text.StringBuilder urlBuilder);
        partial void ProcessResponse(System.Net.Http.HttpClient client, System.Net.Http.HttpResponseMessage response);

        /////////////////////////////////////////////////////
        //
        // A R T I C U L O S
        // 
        //////////////////////////////////////////////////// 

        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<ResArticulo> BusquedaArticuloAsync(string pConexion, int pEmp_Id, int pSuc_id, int pCaja_Id, string pArticulo, int pTipo, string pArticulo_Nombre, int pCliente)
        {
            return BusquedaArticuloAsync(pConexion, pEmp_Id, pSuc_id, pCaja_Id, pArticulo, pTipo, pArticulo_Nombre, pCliente, System.Threading.CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async System.Threading.Tasks.Task<ResArticulo> BusquedaArticuloAsync(string pConexion, int pEmp_Id, int pSuc_id, int pCaja_Id, string pArticulo, int pTipo, string pArticulo_Nombre, int pCliente, System.Threading.CancellationToken cancellationToken)
        {
            if (pConexion == null)
                throw new System.ArgumentNullException("pConexion");

            if (pEmp_Id == null)
                throw new System.ArgumentNullException("pEmp_Id");

            if (pSuc_id == null)
                throw new System.ArgumentNullException("pSuc_id");

            if (pCaja_Id == null)
                throw new System.ArgumentNullException("pCaja_Id");

            if (pArticulo == null)
                throw new System.ArgumentNullException("pArticulo");

            if (pTipo == null)
                throw new System.ArgumentNullException("pTipo");

            if (pArticulo_Nombre == null)
                throw new System.ArgumentNullException("pArticulo_Nombre");

            if (pCliente == null)
                throw new System.ArgumentNullException("pCliente");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/RestApiMobile/Mobile/ArticuloController/BusquedaArticulo?");
            urlBuilder_.Append(System.Uri.EscapeDataString("pConexion") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pConexion, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("pEmp_Id") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pEmp_Id, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("pSuc_id") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pSuc_id, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("pCaja_Id") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pCaja_Id, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("pArticulo") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pArticulo, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("pTipo") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pTipo, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("pArticulo_Nombre") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pArticulo_Nombre, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("pCliente") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pCliente, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Length--;

            var client_ = _httpClient;
            var disposeClient_ = false;
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    request_.Method = new System.Net.Http.HttpMethod("GET");
                    request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    var disposeResponse_ = true;
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = (int)response_.StatusCode;
                        if (status_ == 200)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<ResArticulo>(response_, headers_).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            return objectResponse_.Object;
                        }
                        else
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + status_ + ").", status_, responseData_, headers_, null);
                        }
                    }
                    finally
                    {
                        if (disposeResponse_)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (disposeClient_)
                    client_.Dispose();
            }
        }
        /////////////////////////////////////////////////////
        //
        // C U E N T A 
        // 
        ////////////////////////////////////////////////////

        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<JObject> SocioRegistroAsync(string pIdentificador, string pNombre, string pApellidoPaterno, string pApellidoMaterno, string pTelefono, string pCorreo, string pFechaNacimiento, string pCodigoPostal, string pClaveSocio, string pSexo)
        {
            return SocioRegistroAsync(pIdentificador, pNombre, pApellidoPaterno, pApellidoMaterno, pTelefono, pCorreo, pFechaNacimiento, pCodigoPostal, pClaveSocio,pSexo, System.Threading.CancellationToken.None);
        }

        public async System.Threading.Tasks.Task<JObject> SocioRegistroAsync(string pIdentificador, string pNombre, string pApellidoPaterno, string pApellidoMaterno, string pTelefono, string pCorreo, string pFechaNacimiento, string pCodigoPostal, string pClaveSocio,string pSexo, System.Threading.CancellationToken cancellationToken)
        {
            if (pIdentificador == null)
                throw new System.ArgumentNullException("pIdentificador");

            if (pNombre == null)
                throw new System.ArgumentNullException("pNombre");

            if (pApellidoPaterno == null)
                throw new System.ArgumentNullException("pApellidoPaterno");

            if (pApellidoMaterno == null)
                throw new System.ArgumentNullException("pApellidoMaterno");

            if (pTelefono == null)
                throw new System.ArgumentNullException("pTelefono");

            if (pCorreo == null)
                throw new System.ArgumentNullException("pCorreo");

            if (pFechaNacimiento == null)
                throw new System.ArgumentNullException("pFechaNacimiento");

            if (pCodigoPostal == null)
                throw new System.ArgumentNullException("pCodigoPostal");

            if (pClaveSocio == null)
                throw new System.ArgumentNullException("pClaveSocio");

            if (pSexo == null)
                throw new System.ArgumentNullException("pSexo");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/RestApiMobile/Mobile/LealtadController/Socio_Registro?");
            urlBuilder_.Append(System.Uri.EscapeDataString("pIdentificador") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pIdentificador, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("pNombre") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pNombre, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("pApellidoPaterno") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pApellidoPaterno, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("pApellidoMaterno") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pApellidoMaterno, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("pTelefono") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pTelefono, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("pCorreo") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pCorreo, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("pFechaNacimiento") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pFechaNacimiento, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("pCodigoPostal") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pCodigoPostal, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("pClaveSocio") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pClaveSocio, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("pSexo") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pClaveSocio, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Length--;

            var client_ = _httpClient;
            var disposeClient_ = false;
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    request_.Method = new System.Net.Http.HttpMethod("GET");
                    request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    var disposeResponse_ = true;
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = (int)response_.StatusCode;
                        if (status_ == 200)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<JObject>(response_, headers_).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            return objectResponse_.Object;
                        }
                        else
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + status_ + ").", status_, responseData_, headers_, null);
                        }
                    }
                    finally
                    {
                        if (disposeResponse_)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (disposeClient_)
                    client_.Dispose();
            }
        }

        ////////////////////////////
        ///
        /// L O G I N
        ///
        ///////////////////////////////
        public System.Threading.Tasks.Task<JObject> SocioLoginAsync(string pIdentificador, string pClave)
        {
            return SocioLoginAsync(pIdentificador, pClave, System.Threading.CancellationToken.None);
        }

        public async System.Threading.Tasks.Task<JObject> SocioLoginAsync(string pIdentificador, string pClave, System.Threading.CancellationToken cancellationToken)
        {
            if (pIdentificador == null)
                throw new System.ArgumentNullException("pIdentificador");

            if (pClave == null)
                throw new System.ArgumentNullException("pClave");




            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/RestApiMobile/Mobile/LealtadController/Socio_Login?");
            urlBuilder_.Append(System.Uri.EscapeDataString("pIdentificador") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pIdentificador, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("pClave") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pClave, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Length--;

            var client_ = _httpClient;
            var disposeClient_ = false;
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    request_.Method = new System.Net.Http.HttpMethod("GET");
                    request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    var disposeResponse_ = true;
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = (int)response_.StatusCode;
                        if (status_ == 200)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<JObject>(response_, headers_).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            return objectResponse_.Object;
                        }
                        else
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + status_ + ").", status_, responseData_, headers_, null);
                        }
                    }
                    finally
                    {
                        if (disposeResponse_)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (disposeClient_)
                    client_.Dispose();
            }
        }

        ////////////////////////////
        ///
        /// A V I S O
        ///
        ///////////////////////////////
        public System.Threading.Tasks.Task<string> GetAvisoAsync(string pConexion, string pEmp_id)
        {
            return GetAvisoAsync(pConexion, pEmp_id, System.Threading.CancellationToken.None);
        }

        public async System.Threading.Tasks.Task<string> GetAvisoAsync(string pConexion, string pEmp_id, System.Threading.CancellationToken cancellationToken)
        {
            if (pConexion == null)
                throw new System.ArgumentNullException("pConexion");

            if (pEmp_id == null)
                throw new System.ArgumentNullException("pEmp_id");


            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/RestApiMobile/Mobile/EmpresaController/Aviso_Privacidad?");
            urlBuilder_.Append(System.Uri.EscapeDataString("pConexion") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pConexion, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("pEmp_id") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pEmp_id, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Length--;

            var client_ = _httpClient;
            var disposeClient_ = false;
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    request_.Method = new System.Net.Http.HttpMethod("GET");
                    request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    var disposeResponse_ = true;
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = (int)response_.StatusCode;
                        if (status_ == 200)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<string>(response_, headers_).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            return objectResponse_.Object;
                        }
                        else
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + status_ + ").", status_, responseData_, headers_, null);
                        }
                    }
                    finally
                    {
                        if (disposeResponse_)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (disposeClient_)
                    client_.Dispose();
            }
        }

        /////////////////////////////////////////////////////
        //
        // L A D A
        //
        ////////////////////////////////////////////////////

        public System.Threading.Tasks.Task<List<Ladas>> GetLadaAsync()
        {
            return GetLadaAsync(System.Threading.CancellationToken.None);
        }

        public async System.Threading.Tasks.Task<List<Ladas>> GetLadaAsync(System.Threading.CancellationToken cancellationToken)
        {




            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/anubhavshrimal/75f6183458db8c453306f93521e93d37/raw");
            urlBuilder_.Append("/f77e7598a8503f1f70528ae1cbf9f66755698a16/CountryCodes.json").Append("&");
            urlBuilder_.Length--;

            var client_ = _httpClient;
            var disposeClient_ = false;
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    request_.Method = new System.Net.Http.HttpMethod("GET");
                    request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    var disposeResponse_ = true;
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = (int)response_.StatusCode;
                        if (status_ == 200)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<List<Ladas>>(response_, headers_).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            return objectResponse_.Object;
                        }
                        else
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + status_ + ").", status_, responseData_, headers_, null);
                        }
                    }
                    finally
                    {
                        if (disposeResponse_)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (disposeClient_)
                    client_.Dispose();
            }
        }

        /////////////////////////////////////////////////////
        //
        // E S T A D O 
        //
        ////////////////////////////////////////////////////

        public System.Threading.Tasks.Task<string> GetEstadoAsync()
        {
            return GetEstadoAsync(System.Threading.CancellationToken.None);
        }

        public async System.Threading.Tasks.Task<string> GetEstadoAsync(System.Threading.CancellationToken cancellationToken)
        {




            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/RestApiMobile/Mobile/EmpresaController/Nivel1?");
            urlBuilder_.Append("pConexion=").Append("&");
            urlBuilder_.Length--;

            var client_ = _httpClient;
            var disposeClient_ = false;
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    request_.Method = new System.Net.Http.HttpMethod("GET");
                    request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    var disposeResponse_ = true;
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = (int)response_.StatusCode;
                        if (status_ == 200)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<string>(response_, headers_).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            return objectResponse_.Object;
                        }
                        else
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + status_ + ").", status_, responseData_, headers_, null);
                        }
                    }
                    finally
                    {
                        if (disposeResponse_)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (disposeClient_)
                    client_.Dispose();
            }
        }

        /////////////////////////////////////////////////////
        //
        // C O L O N I A
        //
        ////////////////////////////////////////////////////

        public System.Threading.Tasks.Task<string> GetColoniaAsync(int pNivel1, int pNivel2)
        {
            return GetColoniaAsync(pNivel1, pNivel2, System.Threading.CancellationToken.None);
        }

        public async System.Threading.Tasks.Task<string> GetColoniaAsync(int pNivel1, int pNivel2, System.Threading.CancellationToken cancellationToken)
        {

            if (pNivel1 == null)
                throw new System.ArgumentNullException("pNivel1");
            if (pNivel2 == null)
                throw new System.ArgumentNullException("pNivel2");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/RestApiMobile/Mobile/EmpresaController/Nivel3?");
            urlBuilder_.Append("pConexion=").Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("pNivel1") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pNivel1, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("pNivel2") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pNivel2, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Length--;

            var client_ = _httpClient;
            var disposeClient_ = false;
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    request_.Method = new System.Net.Http.HttpMethod("GET");
                    request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    var disposeResponse_ = true;
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = (int)response_.StatusCode;
                        if (status_ == 200)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<string>(response_, headers_).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            return objectResponse_.Object;
                        }
                        else
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + status_ + ").", status_, responseData_, headers_, null);
                        }
                    }
                    finally
                    {
                        if (disposeResponse_)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (disposeClient_)
                    client_.Dispose();
            }
        }

        /////////////////////////////////////////////////////
        //
        // M U N I C I P I O 
        //
        ////////////////////////////////////////////////////

        public System.Threading.Tasks.Task<string> GetMunicipioAsync(int pNivel1)
        {
            return GetMunicipioAsync(pNivel1, System.Threading.CancellationToken.None);
        }

        public async System.Threading.Tasks.Task<string> GetMunicipioAsync(int pNivel1, System.Threading.CancellationToken cancellationToken)
        {

            if (pNivel1 == null)
                throw new System.ArgumentNullException("pNivel1");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/RestApiMobile/Mobile/EmpresaController/Nivel2?");
            urlBuilder_.Append("pConexion=").Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("pNivel1") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pNivel1, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Length--;

            var client_ = _httpClient;
            var disposeClient_ = false;
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    request_.Method = new System.Net.Http.HttpMethod("GET");
                    request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    var disposeResponse_ = true;
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = (int)response_.StatusCode;
                        if (status_ == 200)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<string>(response_, headers_).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            return objectResponse_.Object;
                        }
                        else
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + status_ + ").", status_, responseData_, headers_, null);
                        }
                    }
                    finally
                    {
                        if (disposeResponse_)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (disposeClient_)
                    client_.Dispose();
            }
        }

        /////////////////////////////////////////////////////
        //
        // L O G O
        // 
        ////////////////////////////////////////////////////

        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<string> CajaLogoAsync(string pConexion, int pEmp_Id, int pSuc_Id, int pCaja_Id)
        {
            return CajaLogoAsync(pConexion, pEmp_Id, pSuc_Id, pCaja_Id, System.Threading.CancellationToken.None);
        }

        public async System.Threading.Tasks.Task<string> CajaLogoAsync(string pConexion, int pEmp_Id, int pSuc_Id, int pCaja_Id, System.Threading.CancellationToken cancellationToken)
        {
            if (pConexion == null)
                throw new System.ArgumentNullException("pConexion");

            if (pEmp_Id == null)
                throw new System.ArgumentNullException("pEmp_Id");

            if (pSuc_Id == null)
                throw new System.ArgumentNullException("pSuc_Id");

            if (pCaja_Id == null)
                throw new System.ArgumentNullException("pCaja_Id");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/RestApiMobile/Mobile/CajaController/CajaLogo?");
            urlBuilder_.Append(System.Uri.EscapeDataString("pConexion") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pConexion, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("pEmp_Id") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pEmp_Id, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("pSuc_Id") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pSuc_Id, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("pCaja_Id") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pCaja_Id, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Length--;

            var client_ = _httpClient;
            var disposeClient_ = false;
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    request_.Method = new System.Net.Http.HttpMethod("GET");
                    request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    var disposeResponse_ = true;
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = (int)response_.StatusCode;
                        if (status_ == 200)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<string>(response_, headers_).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            return objectResponse_.Object;
                        }
                        else
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + status_ + ").", status_, responseData_, headers_, null);
                        }
                    }
                    finally
                    {
                        if (disposeResponse_)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (disposeClient_)
                    client_.Dispose();
            }
        }

        protected struct ObjectResponseResult<T>
        {
            public ObjectResponseResult(T responseObject, string responseText)
            {
                this.Object = responseObject;
                this.Text = responseText;
            }

            public T Object { get; }

            public string Text { get; }
        }

        public bool ReadResponseAsString { get; set; }

        protected virtual async System.Threading.Tasks.Task<ObjectResponseResult<T>> ReadObjectResponseAsync<T>(System.Net.Http.HttpResponseMessage response, System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> headers)
        {
            if (response == null || response.Content == null)
            {
                return new ObjectResponseResult<T>(default(T), string.Empty);
            }

            if (ReadResponseAsString)
            {
                var responseText = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                try
                {
                    var typedBody = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(responseText, JsonSerializerSettings);
                    return new ObjectResponseResult<T>(typedBody, responseText);
                }
                catch (Newtonsoft.Json.JsonException exception)
                {
                    var message = "Could not deserialize the response body string as " + typeof(T).FullName + ".";
                    throw new ApiException(message, (int)response.StatusCode, responseText, headers, exception);
                }
            }
            else
            {
                try
                {
                    using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                    using (var streamReader = new System.IO.StreamReader(responseStream))
                    using (var jsonTextReader = new Newtonsoft.Json.JsonTextReader(streamReader))
                    {
                        var serializer = Newtonsoft.Json.JsonSerializer.Create(JsonSerializerSettings);
                        var typedBody = serializer.Deserialize<T>(jsonTextReader);
                        return new ObjectResponseResult<T>(typedBody, string.Empty);
                    }
                }
                catch (Newtonsoft.Json.JsonException exception)
                {
                    var message = "Could not deserialize the response body stream as " + typeof(T).FullName + ".";
                    throw new ApiException(message, (int)response.StatusCode, string.Empty, headers, exception);
                }
            }
        }

        private string ConvertToString(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value == null)
            {
                return null;
            }

            if (value is bool)
            {
                return System.Convert.ToString((bool)value, cultureInfo).ToLowerInvariant();
            }
            else if (value is byte[])
            {
                return System.Convert.ToBase64String((byte[])value);
            }
            else if (value.GetType().IsArray)
            {
                var array = System.Linq.Enumerable.OfType<object>((System.Array)value);
                return string.Join(",", System.Linq.Enumerable.Select(array, o => ConvertToString(o, cultureInfo)));
            }

            var result = System.Convert.ToString(value, cultureInfo);
            return (result is null) ? string.Empty : result;
        }


        [System.CodeDom.Compiler.GeneratedCode("NSwag", "13.8.2.0 (NJsonSchema v10.2.1.0 (Newtonsoft.Json v11.0.0.0))")]
        public partial class ApiException : System.Exception
        {
            public int StatusCode { get; private set; }

            public string Response { get; private set; }

            public System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> Headers { get; private set; }

            public ApiException(string message, int statusCode, string response, System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> headers, System.Exception innerException)
                : base(message + "\n\nStatus: " + statusCode + "\nResponse: \n" + ((response == null) ? "(null)" : response.Substring(0, response.Length >= 512 ? 512 : response.Length)), innerException)
            {
                StatusCode = statusCode;
                Response = response;
                Headers = headers;
            }

            public override string ToString()
            {
                return string.Format("HTTP Response: \n\n{0}\n\n{1}", Response, base.ToString());
            }
        }

        [System.CodeDom.Compiler.GeneratedCode("NSwag", "13.8.2.0 (NJsonSchema v10.2.1.0 (Newtonsoft.Json v11.0.0.0))")]
        public partial class ApiException<TResult> : ApiException
        {
            public TResult Result { get; private set; }

            public ApiException(string message, int statusCode, string response, System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> headers, TResult result, System.Exception innerException)
                : base(message, statusCode, response, headers, innerException)
            {
                Result = result;
            }
        }

        /////////////////////////////////////////////////////
        //
        // P L A N  D E  L E A L T A D
        // 
        ////////////////////////////////////////////////////

        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<ResConsultaSocioFrecuente> Socio_Frecuente_ListaSocioBusquedaAsync(string pConexion, int pEmp_Id, int pSuc_id, int pCaja_id, string pFiltro)
        {
            return Socio_Frecuente_ListaSocioBusquedaAsync(pConexion, pEmp_Id, pSuc_id, pCaja_id, pFiltro, System.Threading.CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async System.Threading.Tasks.Task<ResConsultaSocioFrecuente> Socio_Frecuente_ListaSocioBusquedaAsync(string pConexion, int pEmp_Id, int pSuc_Id, int pCaja_id, string pFiltro, System.Threading.CancellationToken cancellationToken)
        {
            if (pConexion == null)
                throw new System.ArgumentNullException("pConexion");

            if (pEmp_Id == null)
                throw new System.ArgumentNullException("pEmp_Id");

            if (pSuc_Id == null)
                throw new System.ArgumentNullException("pSuc_Id");

            if (pCaja_id == null)
                throw new System.ArgumentNullException("pCaja_id");

            if (pFiltro == null)
                throw new System.ArgumentNullException("pFiltro");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/RestApiMobile/Mobile/LealtadController/Socio_Frecuente_ListaSocioBusqueda?");
            urlBuilder_.Append(System.Uri.EscapeDataString("pConexion") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pConexion, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("pEmp_Id") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pEmp_Id, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("pSuc_Id") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pSuc_Id, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("pCaja_id") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pCaja_id, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("pFiltro") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pFiltro, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Length--;

            var client_ = _httpClient;
            var disposeClient_ = false;
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    request_.Method = new System.Net.Http.HttpMethod("GET");
                    request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    var disposeResponse_ = true;
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = (int)response_.StatusCode;
                        if (status_ == 200)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<ResConsultaSocioFrecuente>(response_, headers_).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            return objectResponse_.Object;
                        }
                        else
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + status_ + ").", status_, responseData_, headers_, null);
                        }
                    }
                    finally
                    {
                        if (disposeResponse_)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (disposeClient_)
                    client_.Dispose();
            }
        }

        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<ResDetalleSocioFrecuente> Socio_Frecuente_DetalleAsync(string pConexion, int pEmp_Id, int pSuc_id, int pCaja_id, int pSocio_Id)
        {
            return Socio_Frecuente_DetalleAsync(pConexion, pEmp_Id, pSuc_id, pCaja_id, pSocio_Id, System.Threading.CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async System.Threading.Tasks.Task<ResDetalleSocioFrecuente> Socio_Frecuente_DetalleAsync(string pConexion, int pEmp_Id, int pSuc_Id, int pCaja_id, int pSocio_Id, System.Threading.CancellationToken cancellationToken)
        {
            if (pConexion == null)
                throw new System.ArgumentNullException("pConexion");

            if (pEmp_Id == null)
                throw new System.ArgumentNullException("pEmp_Id");

            if (pSuc_Id == null)
                throw new System.ArgumentNullException("pSuc_Id");

            if (pCaja_id == null)
                throw new System.ArgumentNullException("pCaja_id");

            if (pSocio_Id == null)
                throw new System.ArgumentNullException("pSocio_Id");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/RestApiMobile/Mobile/LealtadController/Socio_Frecuente_Detalle?");
            urlBuilder_.Append(System.Uri.EscapeDataString("pConexion") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pConexion, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("pEmp_Id") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pEmp_Id, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("pSuc_Id") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pSuc_Id, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("pCaja_id") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pCaja_id, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("pSocio_Id") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pSocio_Id, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Length--;

            var client_ = _httpClient;
            var disposeClient_ = false;
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    request_.Method = new System.Net.Http.HttpMethod("GET");
                    request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    var disposeResponse_ = true;
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = (int)response_.StatusCode;
                        if (status_ == 200)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<ResDetalleSocioFrecuente>(response_, headers_).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            return objectResponse_.Object;
                        }
                        else
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + status_ + ").", status_, responseData_, headers_, null);
                        }
                    }
                    finally
                    {
                        if (disposeResponse_)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (disposeClient_)
                    client_.Dispose();
            }
        }

        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<string> PagoMonederoAsync(string pConexion, int pEmp_Id, int pAutorizador_Id, int pSuc_Id, int pCaja_Id, long pReferencia, string pCajero, string pMonto, string pOrigen, int pTipoDoc_id, string pCliente, bool pNo_Acumula_Lealtad)
        {
            return PagoMonederoAsync(pConexion, pEmp_Id, pAutorizador_Id, pSuc_Id, pCaja_Id, pReferencia, pCajero, pMonto, pOrigen, pTipoDoc_id, pCliente, pNo_Acumula_Lealtad, System.Threading.CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async System.Threading.Tasks.Task<string> PagoMonederoAsync(string pConexion, int pEmp_Id, int pAutorizador_Id, int pSuc_Id, int pCaja_Id, long pReferencia, string pCajero, string pMonto, string pOrigen, int pTipoDoc_id, string pCliente, bool pNo_Acumula_Lealtad, System.Threading.CancellationToken cancellationToken)
        {
            if (pConexion == null)
                throw new System.ArgumentNullException("pConexion");

            if (pEmp_Id == null)
                throw new System.ArgumentNullException("pEmp_Id");

            if (pAutorizador_Id == null)
                throw new System.ArgumentNullException("pAutorizador_Id");

            if (pSuc_Id == null)
                throw new System.ArgumentNullException("pSuc_Id");

            if (pCaja_Id == null)
                throw new System.ArgumentNullException("pCaja_Id");

            if (pReferencia == null)
                throw new System.ArgumentNullException("pReferencia");

            if (pCajero == null)
                throw new System.ArgumentNullException("pCajero");

            if (pMonto == null)
                throw new System.ArgumentNullException("pMonto");

            if (pOrigen == null)
                throw new System.ArgumentNullException("pOrigen");

            if (pTipoDoc_id == null)
                throw new System.ArgumentNullException("pTipoDoc_id");

            if (pCliente == null)
                throw new System.ArgumentNullException("pCliente");

            if (pNo_Acumula_Lealtad == null)
                throw new System.ArgumentNullException("pNo_Acumula_Lealtad");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/RestApiMobile/Mobile/LealtadController/Pago_Monedero?");
            urlBuilder_.Append(System.Uri.EscapeDataString("pConexion") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pConexion, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("pEmp_Id") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pEmp_Id, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("pAutorizador_Id") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pAutorizador_Id, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("pSuc_id") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pSuc_Id, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("pCaja_Id") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pCaja_Id, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("pReferencia") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pReferencia, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("pCajero") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pCajero, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("pMonto") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pMonto, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("pOrigen") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pOrigen, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("pTipoDoc_id") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pTipoDoc_id, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("pCliente") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pCliente, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("pNo_Acumula_Lealtad") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pNo_Acumula_Lealtad, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Length--;

            var client_ = _httpClient;
            var disposeClient_ = false;
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    request_.Method = new System.Net.Http.HttpMethod("GET");
                    request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    var disposeResponse_ = true;
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = (int)response_.StatusCode;
                        if (status_ == 200)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<string>(response_, headers_).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            return objectResponse_.Object;
                        }
                        else
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + status_ + ").", status_, responseData_, headers_, null);
                        }
                    }
                    finally
                    {
                        if (disposeResponse_)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (disposeClient_)
                    client_.Dispose();
            }
        }

        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<ResBonificacionMonedero> BonificacionMonederoAsync(string pConexion, int pEmp_Id, int pAutorizador_Id, int pSuc_Id, int pCaja_Id, int pTicket, long pReferencia, string pCajero, decimal pMonto, string pOrigen, string pProductos, int pTipoDoc_id, string pCliente, bool pNo_Acumula_Lealtad, string pListaFormaPago)
        {
            return BonificacionMonederoAsync(pConexion, pEmp_Id, pAutorizador_Id, pSuc_Id, pCaja_Id, pTicket, pReferencia, pCajero, pMonto, pOrigen, pProductos, pTipoDoc_id, pCliente, pNo_Acumula_Lealtad, pListaFormaPago, System.Threading.CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async System.Threading.Tasks.Task<ResBonificacionMonedero> BonificacionMonederoAsync(string pConexion, int pEmp_Id, int pAutorizador_Id, int pSuc_Id, int pCaja_Id, int pTicket, long pReferencia, string pCajero, decimal pMonto, string pOrigen, string pProductos, int pTipoDoc_id, string pCliente, bool pNo_Acumula_Lealtad, string pListaFormaPago, System.Threading.CancellationToken cancellationToken)
        {
            if (pConexion == null)
                throw new System.ArgumentNullException("pConexion");

            if (pEmp_Id == null)
                throw new System.ArgumentNullException("pEmp_Id");

            if (pAutorizador_Id == null)
                throw new System.ArgumentNullException("pAutorizador_Id");

            if (pSuc_Id == null)
                throw new System.ArgumentNullException("pSuc_Id");

            if (pCaja_Id == null)
                throw new System.ArgumentNullException("pCaja_Id");

            if (pTicket == null)
                throw new System.ArgumentNullException("pTicket");

            if (pReferencia == null)
                throw new System.ArgumentNullException("pReferencia");

            if (pCajero == null)
                throw new System.ArgumentNullException("pCajero");

            if (pMonto == null)
                throw new System.ArgumentNullException("pMonto");

            if (pOrigen == null)
                throw new System.ArgumentNullException("pOrigen");

            if (pProductos == null)
                throw new System.ArgumentNullException("pProductos");

            if (pTipoDoc_id == null)
                throw new System.ArgumentNullException("pTipoDoc_id");

            if (pCliente == null)
                throw new System.ArgumentNullException("pCliente");

            if (pNo_Acumula_Lealtad == null)
                throw new System.ArgumentNullException("pNo_Acumula_Lealtad");

            if (pListaFormaPago == null)
                throw new System.ArgumentNullException("pListaFormaPago");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/RestApiMobile/Mobile/LealtadController/Bonificacion_Monedero?");
            urlBuilder_.Append(System.Uri.EscapeDataString("pConexion") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pConexion, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("pEmp_Id") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pEmp_Id, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("pAutorizador_Id") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pAutorizador_Id, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("pSuc_id") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pSuc_Id, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("pCaja_Id") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pCaja_Id, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("pTicket") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pTicket, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("pReferencia") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pReferencia, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("pCajero") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pCajero, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("pMonto") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pMonto, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("pOrigen") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pOrigen, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("pProductos") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pProductos, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("pTipoDoc_id") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pTipoDoc_id, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("pCliente") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pCliente, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("pNo_Acumula_Lealtad") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pNo_Acumula_Lealtad, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("pListaFormaPago") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pListaFormaPago, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Length--;

            var client_ = _httpClient;
            var disposeClient_ = false;
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    request_.Method = new System.Net.Http.HttpMethod("GET");
                    request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    var disposeResponse_ = true;
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = (int)response_.StatusCode;
                        if (status_ == 200)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<ResBonificacionMonedero>(response_, headers_).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            return objectResponse_.Object;
                        }
                        else
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + status_ + ").", status_, responseData_, headers_, null);
                        }
                    }
                    finally
                    {
                        if (disposeResponse_)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (disposeClient_)
                    client_.Dispose();
            }
        }


        /////////////////////////////////////////////////////
        //
        // S U C U R S A L - U B I C A C I Ó N
        // 
        //////////////////////////////////////////////////// 
        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<string> SucursalUbicacionListaAsync(string pConexion, int pEmp_Id, int pCP)
        {
            return SucursalUbicacionListaAsync(pConexion, pEmp_Id, pCP, System.Threading.CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async System.Threading.Tasks.Task<string> SucursalUbicacionListaAsync(string pConexion, int pEmp_Id, int pCP, System.Threading.CancellationToken cancellationToken)
        {
            if (pConexion == null)
                throw new System.ArgumentNullException("pConexion");

            if (pEmp_Id == null)
                throw new System.ArgumentNullException("pEmp_Id");

            if (pCP == null)
                throw new System.ArgumentNullException("pCP");

            var urlBuilder_ = new System.Text.StringBuilder();   
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/RestApiMobile/Mobile/SucursalController/SucursalUbicacion_List?");
            urlBuilder_.Append(System.Uri.EscapeDataString("pConexion") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pConexion, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("pEmp_Id") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pEmp_Id, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("pCP") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pCP, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Length--;

            var client_ = _httpClient;
            var disposeClient_ = false;
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    request_.Method = new System.Net.Http.HttpMethod("GET");
                    request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    var disposeResponse_ = true;
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = (int)response_.StatusCode;
                        if (status_ == 200)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<string>(response_, headers_).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            return objectResponse_.Object;
                        }
                        else
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + status_ + ").", status_, responseData_, headers_, null);
                        }
                    }
                    finally
                    {
                        if (disposeResponse_)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (disposeClient_)
                    client_.Dispose();
            }
        }



        /////////////////////////////////////////////////////
        ///
        /// V E N T A
        /// 
        ////////////////////////////////////////////////////
        
        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<string> PromocionVentaAsync(string pConexion, int pEmp_Id, int pSuc_id, int pCaja_id, int pCliente, string pDetalle)
        {
            return PromocionVentaAsync(pConexion, pEmp_Id, pSuc_id, pCaja_id, pCliente, pDetalle, System.Threading.CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async System.Threading.Tasks.Task<string> PromocionVentaAsync(string pConexion, int pEmp_Id, int pSuc_Id, int pCaja_id, int pCliente, string pDetalle, System.Threading.CancellationToken cancellationToken)
        {
            if (pConexion == null)
                throw new System.ArgumentNullException("pConexion");

            if (pEmp_Id == null)
                throw new System.ArgumentNullException("pEmp_Id");

            if (pSuc_Id == null)
                throw new System.ArgumentNullException("pSuc_Id");

            if (pCaja_id == null)
                throw new System.ArgumentNullException("pCaja_id");

            if (pCliente == null)
                throw new System.ArgumentNullException("pCliente");

            if (pDetalle == null)
                throw new System.ArgumentNullException("pDetalle");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/RestApiMobile/Mobile/VentaController/PromocionVenta?");
            urlBuilder_.Append(System.Uri.EscapeDataString("pConexion") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pConexion, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("pEmp_Id") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pEmp_Id, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("pSuc_Id") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pSuc_Id, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("pCaja_id") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pCaja_id, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("pCliente") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pCliente, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("pDetalle") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pDetalle, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Length--;

            var client_ = _httpClient;
            var disposeClient_ = false;
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    request_.Method = new System.Net.Http.HttpMethod("GET");
                    request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    var disposeResponse_ = true;
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = (int)response_.StatusCode;
                        if (status_ == 200)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<string>(response_, headers_).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            return objectResponse_.Object;
                        }
                        else
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + status_ + ").", status_, responseData_, headers_, null);
                        }
                    }
                    finally
                    {
                        if (disposeResponse_)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (disposeClient_)
                    client_.Dispose();
            }
        }

        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<ResVenta> VentaInsertAsync(string pConexion, string pEncabezado, string pDetalle, string pFormaPago, string pControlado)
        {
            return VentaInsertAsync(pConexion, pEncabezado, pDetalle, pFormaPago, pControlado, System.Threading.CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async System.Threading.Tasks.Task<ResVenta> VentaInsertAsync(string pConexion, string pEncabezado, string pDetalle, string pFormaPago, string pControlado, System.Threading.CancellationToken cancellationToken)
        {
            if (pConexion == null)
                throw new System.ArgumentNullException("pConexion");

            if (pEncabezado == null)
                throw new System.ArgumentNullException("pEncabezado");

            if (pDetalle == null)
                throw new System.ArgumentNullException("pDetalle");

            if (pFormaPago == null)
                throw new System.ArgumentNullException("pFormaPago");

            if (pControlado == null)
                throw new System.ArgumentNullException("pControlado");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/RestApiMobile/Mobile/VentaController/Venta_Insert?");
            urlBuilder_.Append(System.Uri.EscapeDataString("pConexion") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pConexion, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("pEncabezado") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pEncabezado, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("pDetalle") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pDetalle, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("pFormaPago") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pFormaPago, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("pControlado") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pControlado, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Length--;

            var client_ = _httpClient;
            var disposeClient_ = false;
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    request_.Content = new System.Net.Http.StringContent(string.Empty, System.Text.Encoding.UTF8, "application/json");
                    request_.Method = new System.Net.Http.HttpMethod("POST");
                    request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    var disposeResponse_ = true;
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = (int)response_.StatusCode;
                        if (status_ == 200)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<ResVenta>(response_, headers_).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            return objectResponse_.Object;
                        }
                        else
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + status_ + ").", status_, responseData_, headers_, null);
                        }
                    }
                    finally
                    {
                        if (disposeResponse_)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (disposeClient_)
                    client_.Dispose();
            }
        }

        /////////////////////////////////////////////////////
        //
        // S U C U R S A L _ U B I C A C I O N - C L I C K & C O L L E C T 
        // 
        ////////////////////////////////////////////////////

        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<string> SucursalUbicacion_ListPreventa(string pConexion, int pEmp_Id, long pCP)
        {
            return SucursalUbicacion_ListPreventa(pConexion, pEmp_Id, pCP, System.Threading.CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>OK</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async System.Threading.Tasks.Task<string> SucursalUbicacion_ListPreventa(string pConexion, int pEmp_Id, long pCP, System.Threading.CancellationToken cancellationToken)
        {
            if (pConexion == null)
                throw new System.ArgumentNullException("pConexion");

            if (pEmp_Id == null)
                throw new System.ArgumentNullException("pEmp_Id");

            if (pCP == null)
                throw new System.ArgumentNullException("pCP");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/RestApiMobile/Mobile/SucursalController/SucursalUbicacion_ListPreventa?");
            urlBuilder_.Append(System.Uri.EscapeDataString("pConexion") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pConexion, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("pEmp_Id") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pEmp_Id, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append(System.Uri.EscapeDataString("pCP") + "=").Append(System.Uri.EscapeDataString(ConvertToString(pCP, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Length--;

            var client_ = _httpClient;
            var disposeClient_ = false;
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    request_.Method = new System.Net.Http.HttpMethod("GET");
                    request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    var disposeResponse_ = true;
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = (int)response_.StatusCode;
                        if (status_ == 200)
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<string>(response_, headers_).ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
                            }
                            return objectResponse_.Object;
                        }
                        else
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + status_ + ").", status_, responseData_, headers_, null);
                        }
                    }
                    finally
                    {
                        if (disposeResponse_)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (disposeClient_)
                    client_.Dispose();
            }
        }

    }
}

#pragma warning restore 1591
#pragma warning restore 1573
#pragma warning restore 472
#pragma warning restore 114
#pragma warning restore 108
