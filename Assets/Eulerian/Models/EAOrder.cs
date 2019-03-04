using System;
namespace eulerian
{
    public class EAOrder : EAEstimate
    {
        private static readonly string KEY_ESTIMATE_REF = "estimateref";
        private static readonly string KEY_PAYMENT = "payment";

        public EAOrder(string path, string reference) : base(path, reference)
        {
            json.Remove(KEY_IS_ESTIMATE);
        }

        public void SetPayment(string payment) => json[KEY_PAYMENT] = payment;
        public void SetEstimateRef(string estimateRef) => json[KEY_ESTIMATE_REF] = estimateRef;
    }
}
