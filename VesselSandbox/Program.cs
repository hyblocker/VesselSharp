﻿namespace VesselSandbox
{
	class Program
	{
		static void Main(string[] args)
		{
			using (var game = new SandboxGame())
			{
				game.Run();
			}
		}
	}
}
