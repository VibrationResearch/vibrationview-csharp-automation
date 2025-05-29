using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VibrationVIEWControlPanel;

public class VibrationVIEWAPI
{
	private VibrationVIEWLib.VibrationVIEW _vibrationVIEW;
	private const int MAX_ACTIVE_X_SUPPORTED_CHANNEL_COUNT = 64;

	public VibrationVIEWAPI()
	{
		_vibrationVIEW = new VibrationVIEWLib.VibrationVIEW();
	}
	public void OpenTest()
	{
		_vibrationVIEW.MenuCommand(MenuCommandIds.OpenTest);
	}

	public void StartTest()
	{
		_vibrationVIEW.StartTest();
	}

	public void StopTest()
	{
		_vibrationVIEW.StopTest();
	}

	public void EditTest(string filePath)
	{
		_vibrationVIEW.EditTest(filePath);
	}

	public void SaveData()
	{
		_vibrationVIEW.MenuCommand(MenuCommandIds.SaveAs);
	}

	public int GetChannelCount()
	{
		return _vibrationVIEW.HardwareInputChannels;
	}

	public string[] GetChannelUnitCategoryLabels()
	{
		int count = GetChannelCount();
		string[] labels = new string[count];

		for (int i = 0; i < count; i++)
		{
			labels[i] = _vibrationVIEW.ChannelLabel[i];
		}
		return labels;
	}

	public List<string> GetChannelUnits()
	{
		int count = GetChannelCount();
		var units = new List<string>();
		for (int i = 0; i < count; i++)
		{
			units.Add(_vibrationVIEW.ChannelUnit[i]);
		}
		return units;
	}


	public Array GetChannelValues()
	{
		int count = GetChannelCount();

		Array arr = Array.CreateInstance(typeof(float), count);
		_vibrationVIEW.Channel(ref arr);

		return arr;
	}

	public Array GetDemandValues()
	{
		Array arr = Array.CreateInstance(typeof(float), 1);
		_vibrationVIEW.Demand(ref arr);

		return arr;
	}


	public Array GetControlValues()
	{
		Array arr = Array.CreateInstance(typeof(float), 1);
		_vibrationVIEW.Control(ref arr);

		return arr;
	}

	public List<string> GetControlUnits()
	{
		int count = GetChannelCount();
		var units = new List<string>();
		for (int i = 0; i < count; i++)
		{
			units.Add(_vibrationVIEW.ControlUnit[i]);
		}
		return units;
	}

	public string ReportField(string fieldCode)
	{
		return _vibrationVIEW.ReportField[fieldCode];
	}

	public (string status, int stopCode) GetStatus()
	{
		string status = "";
		int code = 0;

		_vibrationVIEW.Status(out status, out code);
		return (status, code);
	}

	public List<string> GetAllChannelInfo()
	{
		int count = GetChannelCount();

		var channelLabels = new List<string>();

		Array controlValues = GetControlValues();
		Array demandValues = GetDemandValues();
		Array channelValues = GetChannelValues();
		List<string> controlUnits = GetControlUnits();
		List<string> channelUnits = GetChannelUnits();

		channelLabels.Add($"Demand: {demandValues.GetValue(0)} {controlUnits[0]}");
		channelLabels.Add($"Control: {controlValues.GetValue(0)} {controlUnits[0]}");
		for (int i = 0; i < count; i++)
		{
			channelLabels.Add($"Channel {i + 1}: {channelValues.GetValue(i)} {channelUnits[i]}");
		}

		return channelLabels;
	}
}
