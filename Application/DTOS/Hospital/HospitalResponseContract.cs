namespace Application.DTOS.Hospital
{
    public interface HospitalResponseContract : HospitalRequestContract
    {
        public int HospitalId { get; set; }
        public DateTime DataInativacao { get; set; }
    }
}