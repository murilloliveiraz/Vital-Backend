namespace Application.DTOS.Hospital
{
    public class HospitalResponseContract : HospitalRequestContract
    {
        public int HospitalId { get; set; }
        public DateTime DataInativacao { get; set; }
    }
}