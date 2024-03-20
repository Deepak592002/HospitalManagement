import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { HubConnectionBuilder,LogLevel} from '@microsoft/signalr';

const PatientTable = () => {
  const [patients, setPatients] = useState([]);
    const[conn,setConn]=useState()
  useEffect(() => {
    const connection = new HubConnectionBuilder()
      .withUrl("http://localhost:5201/status") // URL to the SignalR hub
      .configureLogging(LogLevel.Information)
      .build();

    connection.start().then(() => {
      console.log("SignalR Connected");

      connection.on("StatusUpdated", (patientId, newStatus) => {
        // Handle status updates
        setPatients(prevPatients =>
          prevPatients.map(patient =>
            patient.patientId === patientId ? { ...patient, status: newStatus } : patient
          )
        );
      });
    }).catch(err => console.error(err));
    setConn(connection)
  }, []);

  const toggleStatus = (patientId, currentStatus) => {
    // Toggle status locally first
    const updatedPatients = patients.map(patient =>
      patient.patientId === patientId ? { ...patient, status: !currentStatus } : patient
    );
    setPatients(updatedPatients);

    // Send toggle status request to server via SignalR
    conn.invoke("UpdateStatus", patientId, !currentStatus)
      .catch(err => console.error(err));
  };

  // Dummy patient data
  const dummyPatients = [
    { patient_id: 1, name: 'John Doe', email: 'john.doe@example.com', instance: 1, status: true },
    { patient_id: 2, name: 'Jane Smith', email: 'jane.smith@example.com', instance: 2, status: false },
    { patient_id: 3, name: 'Alice Johnson', email: 'alice.johnson@example.com', instance: 3, status: true },
    { patient_id: 4, name: 'Bob Williams', email: 'bob.williams@example.com', instance: 1, status: false }
  ];

  useEffect(() => {
    axios.get('http://localhost:5201/api/Patient')
    .then(function (response) {
    // handle success
    console.log(response.data);
    setTimeout(() => {
        setPatients(response.data);
      }, 1000);
  })
 // Simulating a delay of 1 second
  }, []);

  return (
    <div className="container mx-auto">
      <h1 className="text-3xl font-bold text-center mb-8 text-gray-800">Patient Data</h1>
      <div className="overflow-x-auto">
        <table className="min-w-full rounded-lg overflow-hidden border border-gray-300">
          <thead className="bg-gradient-to-r from-purple-500 via-pink-500 to-red-500 text-white">
            <tr>
              <th className="px-6 py-3 text-left text-sm font-semibold uppercase tracking-wider">Name</th>
              <th className="px-6 py-3 text-left text-sm font-semibold uppercase tracking-wider">Email</th>
              <th className="px-6 py-3 text-left text-sm font-semibold uppercase tracking-wider">Instance</th>
              <th className="px-6 py-3 text-left text-sm font-semibold uppercase tracking-wider">Status</th>
            </tr>
          </thead>
          <tbody className="bg-white divide-y divide-gray-300">
            {patients.map((patient) => (
              <tr key={patient.patientId}>
                <td className="px-6 py-4 whitespace-nowrap">{patient.name}</td>
                <td className="px-6 py-4 whitespace-nowrap">{patient.email}</td>
                <td className="px-6 py-4 whitespace-nowrap">{patient.instance}</td>
                <td className="px-6 py-4 whitespace-nowrap">
                  <button className={patient.status ? 'bg-green-500 text-white py-1 px-2 rounded-full text-xs' : 'bg-red-500 text-white py-1 px-2 rounded-full text-xs'}
                    onClick={() => toggleStatus(patient.patientId, patient.status)}>
                    {patient.status ? 'Active' : 'Inactive'}
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
};

export default PatientTable;
