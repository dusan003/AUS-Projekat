﻿using Common;
using Modbus.FunctionParameters;
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;

namespace Modbus.ModbusFunctions
{
    /// <summary>
    /// Class containing logic for parsing and packing modbus read holding registers functions/requests.
    /// </summary>
    public class ReadHoldingRegistersFunction : ModbusFunction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReadHoldingRegistersFunction"/> class.
        /// </summary>
        /// <param name="commandParameters">The modbus command parameters.</param>
        public ReadHoldingRegistersFunction(ModbusCommandParameters commandParameters) : base(commandParameters)
        {
            CheckArguments(MethodBase.GetCurrentMethod(), typeof(ModbusReadCommandParameters));
        }

        public override byte[] PackRequest()
        {
            //TO DO: IMPLEMENT
            byte[] request = new byte[12];
        
            byte[] temp = BitConverter.GetBytes(CommandParameters.TransactionId);
        
            request[0] = temp[1];
            request[1] = temp[0];
        
            temp = BitConverter.GetBytes(CommandParameters.ProtocolId);
            request[2] = temp[1];
            request[3] = temp[0];
        
            temp = BitConverter.GetBytes(CommandParameters.Length);
            request[4] = temp[1];
            request[5] = temp[0];
        
            request[6] = CommandParameters.UnitId;
            request[7] = CommandParameters.FunctionCode;
        
            temp = BitConverter.GetBytes(((ModbusReadCommandParameters)CommandParameters).StartAddress);
            request[8] = temp[1];
            request[9] = temp[0];
        
            temp = BitConverter.GetBytes(((ModbusReadCommandParameters)CommandParameters).Quantity);
            request[10] = temp[1];
            request[11] = temp[0];
        
            return request;
        }
        
        /// <inheritdoc />
        public override Dictionary<Tuple<PointType, ushort>, ushort> ParseResponse(byte[] response)
        {
            //TO DO: IMPLEMENT
            Dictionary<Tuple<PointType, ushort>, ushort> Response = new Dictionary<Tuple<PointType, ushort>, ushort>();
            ushort address = ((ModbusReadCommandParameters)CommandParameters).StartAddress;
            for (int i = 0; i < response[8] / 2; i++)
            {
                ushort data = BitConverter.ToUInt16(response, 9 + i * 2);
                Response.Add(new Tuple<PointType, ushort>(PointType.ANALOG_OUTPUT, address++), (ushort)IPAddress.NetworkToHostOrder((short)data));
        
            }
        
            return Response;
        }
    }
}
