using Entities.Dtos.AppointmentDto;
using Entities.Dtos.PatientDto;
using Entities.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Tests
{
    public class HealthWebApiTest
    {
        public static string Token { get; set; }

        // orientaçao para todos os testes, todos os testes necessitam de um token de autenticação para acesso as rotas, o token é gerado automaticamente com as credenciais do usuário padrão (Login: DefaultUser, Password: Default@123), caso esse usuário seja deletado da base de dados, é necessário alterar as credenciais na função GetToken

        // teste de retorno de todos os pacientes da base de dados
        [Fact]
        public async void GetPatientsTest()
        {
            //Arrange
            string url = "https://localhost:7018/Health/v1/Patients/GetAllViews";

            //Act
            var result = await Get(url);

            //Assert
            Assert.True(result.Any());
        }


        // teste de criação de uma consulta na base de dados
        [Fact]
        public async void CreateAppointmentTest()
        {
            //Arrange
            string url = "https://localhost:7018/Health/v1/Appointments/CreateAppointment";
            int doctorId = 1;
            int patientId = 1;
            var appointmentrequest = new
            {
                DoctorId = doctorId,
                PatientId = patientId,
                AppointmentDate = DateTime.Now.AddDays(1)
            };

            //Act
            AppointmentView result = await CreateNewAppointment(url, appointmentrequest);

            //Assert
            Assert.True(result.Status.Equals(AppointmentStatus.Scheduled));
        }


        // teste de validação de criação de uma consulta inválida
        [Fact]
        public async Task ValidationDateAppointmentTest()
        {
            //Arrange
            string url = "https://localhost:7018/Health/v1/Appointments/CreateAppointment";
            int doctorId = 1;
            int patientId = 1;
            var appointmentrequest = new
            {
                DoctorId = doctorId,
                PatientId = patientId,
                AppointmentDate = DateTime.Now.AddMinutes(-1),
            };

            //Act
            var exception = await Assert.ThrowsAsync<Exception>(() => CreateNewAppointment(url, appointmentrequest));


        }


        // Função para gerar o token a partir dos dados de um usuário da base de dados
        public void GetToken()
        {
            string urlApiLogin = "https://localhost:7018/Health/v1/Users/Login";

            using (var client = new HttpClient())
            {
                string login = "DefaultUser";
                string password = "Default@123";

                var loginRequest = new
                {
                    UserName = login,
                    Password = password
                };

                string JsonLoginRequest = JsonConvert.SerializeObject(loginRequest);
                var content = new StringContent(JsonLoginRequest, Encoding.UTF8, "application/json");

                var result = client.PostAsync(urlApiLogin, content);

                result.Wait();

                if (result.Result.IsSuccessStatusCode)
                {
                    var tokenJson = result.Result.Content.ReadAsStringAsync().Result;
                    var tokenObject = JsonConvert.DeserializeAnonymousType(tokenJson, new { token = "" });
                    Token = tokenObject.token;
                }
            }
        }


        // Função para consultar todos os pacientes da base de dados
        public async Task<List<PatientView>> Get(string url)
        {
            try
            {
                GetToken();
                if (!string.IsNullOrWhiteSpace(Token))
                {
                    using (var client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

                        var res = await client.GetStringAsync(url);

                        var patients = JsonConvert.DeserializeObject<List<PatientView>>(res).ToList();

                        return patients;
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        // Função para criar/adicionar uma consulta médica na base de dados
        public async Task<AppointmentView> CreateNewAppointment(string url, object createAppointmentRequest = null)
        {
           try
            {
                GetToken();

                var content = ToRequest(createAppointmentRequest);

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                    var res = await client.PostAsync(url, content);
                    var response = await res.Content.ReadAsStringAsync();

                    AppointmentView appointment = JsonConvert.DeserializeObject<AppointmentView>(response);
                    return appointment;
                }
            } catch (Exception ex)
            {
                throw new Exception("Não foi possível agendar a consulta");
            }
        }


        // Função para serializar um objeto em uma string Json
        private static StringContent ToRequest(object request)
        {
            var json = JsonConvert.SerializeObject(request);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            return data;
        }
    }
}