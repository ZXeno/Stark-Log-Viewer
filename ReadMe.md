# Stark Log Viewer

Stark Log Viewer was birthed from a deep dissatisfaction with existing log viewers that were freely available and not tied into an existing solution or product on the market. This open-source viewer is inteded to be simple and fast, with a minimalist feature set, but powerful enough to sort and read Windows event logs quickly. No bloatware, open-source, and using a tech stack you probably won't expect.

### Roadmap
There's more in the works, but a firm roadmap hasn't been created yet. As of right now it's very simple, quickly thrown together, and still a bit messy at the moment. However, active development is on-going. There are some features planned that are in the works.

Planned features:
  * Sort by column contents
  * Filter by each column type
  * Save to CSV, Plaintext, or JSON format
  * Copy more than one log to clipboard at a time.
  * Dark Mode

### Contribution
If you find any issues, please open an issue in the tracker. If you would like to contribute code, please fork the project and submit a pull request!

Currently, things that this project needs that are not in th feature list but need to be done: tests, startup performance improvements, documentation, and someone with a sense for UI style.

##### Setting Up The Dev Environment
First, you'll need to set up [Electron.NET](https://github.com/ElectronNET/Electron.NET).
1. You'll need to install the Electron.Net tooling locally using the below command:
		`dotnet tool install ElectronNET.CLI -g`

2. Build the project. 
3. After you build, run `electronize start` from the command line in the project directory.

##### Building the deployment package(s)
Electron.NET has it's own documentaiton for this, but the quick and dirty is that you can build using the following command:

		electronize build /target win

Conversely, if you're planning on reading remote Windows event logs from a different platform, you're free to build using the `osx` or `linux` build targets. This should place the build under **/bin/desktop** folder.