use ParkingLot

select * from DriverTypeTable

insert into DriverTypeTable values
('Policeman',100),
('Security',180),
('Owner',280),
('Driver',260)


alter table ParkingTable
alter column ExitTime dateTime

select * from [dbo].[ParkingResponseTable]

select * from [dbo].[ParkingTypeTable]
select * from [dbo].[VehicalTypeTable]

JOIN QUERY 

 public ParkingResponse CalculateCharge(int parkingId)
        {
            var result = from parkingModel in parkingContext.ParkingTable
                         join parkingTypeModel in parkingContext.ParkingTypeTable
                         on parkingModel.ParkTypeID equals parkingTypeModel.ParkTypeID
                         join driverTypeModel in parkingContext.DriverTypeTable
                         on parkingModel.ParkTypeID equals driverTypeModel.DriverTypeID
                         join vehicalTypeModel in parkingContext.VehicalTypeTable
                         on parkingModel.VehicleTypeID equals vehicalTypeModel.VehicleTypeID
                         select new ParkingResponse()
                         {
                             ParkingId = parkingModel.ParkingId,
                             VehicalNo = parkingModel.VehicalNo,
                             SlotNo = parkingModel.SlotNo,
                             IsEmpty = parkingModel.IsEmpty,
                             EntryTime = parkingModel.EntryTime,
                             ExitTime = parkingModel.ExitTime,
                             ParkingTypeCharges = parkingTypeModel.Charges,
                             VehicalTypeCharges = vehicalTypeModel.Charges,
                             DriverTypeCharges = driverTypeModel.Charges,
                             ParkType = parkingTypeModel.ParkType,
                             DriverType = driverTypeModel.DriverType,
                             VehicleType = vehicalTypeModel.VehicalType,
                             TotalCharges = 0,
                         };
            foreach (var data in result)
            {
                if (data.ParkingId == parkingId)
                {
                    return data;
                }
            }
            return null;
        }