using System;
using Modbus.IO;
using Unme.Common;

namespace Modbus.Device
{
	/// <summary>
	/// Modbus device.
	/// </summary>
	public abstract class ModbusDevice
	{
		private ModbusTransport _transport;

		internal ModbusDevice(ModbusTransport transport)
		{
			_transport = transport;
		}

		/// <summary>
		/// Gets the Modbus Transport.
		/// </summary>
		/// <value>The transport.</value>
		public ModbusTransport Transport
		{
			get
			{
				return _transport;
			}
		}

		/// <summary>
		/// Releases unmanaged and - optionally - managed resources
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Releases unmanaged and - optionally - managed resources
		/// </summary>
		/// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
				DisposableUtility.Dispose(ref _transport);
		}
	}
}
