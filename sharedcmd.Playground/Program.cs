using System;

using sharedcmd;

dynamic cmd = new Cmd();
cmd.git.log["--grep"]("test");
cmd.git.log("test", grep: "--");
cmd.git.log(grep: "--", test: true);
cmd.git.log["--grep test"]();
cmd["git log --grep test"]();