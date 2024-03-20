using HospitalManagement.Data;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Hubs
{
    public class PatientStatusHub:Hub
    {
        private readonly HospitalManagementContext dbContext;
        public PatientStatusHub(HospitalManagementContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task UpdateStatus(int patientId, bool newStatus)
        {
            // Broadcast the updated status to all connected clients
            var patient = await dbContext.PatientData.FindAsync(patientId);
            if (patient != null)
            {
                patient.Status = newStatus;
                await dbContext.SaveChangesAsync();
            }
            await Clients.All.SendAsync("StatusUpdated", patientId, newStatus);
        }

    }
}
