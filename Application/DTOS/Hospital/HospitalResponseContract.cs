namespace Application.DTOS.Hospital
{
    public class HospitalResponseContract : HospitalRequestContract
    {
        public int Id { get; set; }
        public DateTime DataInativacao { get; set; }
    }
}