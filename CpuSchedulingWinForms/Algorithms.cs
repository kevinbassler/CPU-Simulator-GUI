using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace CpuSchedulingWinForms
{
    public static class Algorithms
    {
        private static Random rand = new Random();

        //added longest job first algorithm
        public static void ljfAlgorithm(string userInput)
        {
            int np = Convert.ToInt16(userInput);
            double[] bp = new double[np];
            double[] wtp = new double[np];
            double[] sortedBp = new double[np];
            double twt = 0.0, awt;
            bool found = false;

            DialogResult result = MessageBox.Show("Longest Job First Scheduling", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                for (int i = 0; i < np; i++)
                {
                    bp[i] = rand.Next(1, 21); // random burst time between 1–20
                    sortedBp[i] = bp[i];
                }

                Array.Sort(sortedBp);
                Array.Reverse(sortedBp);

                for (int i = 0; i < np; i++)
                {
                    if (i == 0)
                    {
                        for (int j = 0; j < np; j++)
                        {
                            if (sortedBp[i] == bp[j] && !found)
                            {
                                wtp[i] = 0;
                                bp[j] = -1;
                                found = true;
                            }
                        }
                        found = false;
                    }
                    else
                    {
                        for (int j = 0; j < np; j++)
                        {
                            if (sortedBp[i] == bp[j] && !found)
                            {
                                wtp[i] = wtp[i - 1] + sortedBp[i - 1];
                                bp[j] = -1;
                                found = true;
                            }
                        }
                        found = false;
                    }
                }

                for (int i = 0; i < np; i++)
                {
                    twt += wtp[i];
                }
                awt = twt / np;
                MessageBox.Show("Average waiting time = " + awt + " sec(s)", "Average Waiting Time", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //added highest response ratio nex algorithm
        public static void hrrnAlgorithm(string userInput)
        {
            int np = Convert.ToInt16(userInput);
            double[] arrival = new double[np];
            double[] burst = new double[np];
            bool[] completed = new bool[np];
            double[] waiting = new double[np];
            double[] turnaround = new double[np];

            double currentTime = 0;
            int completedCount = 0;

            DialogResult result = MessageBox.Show("Highest Response Ratio Next Scheduling", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                for (int i = 0; i < np; i++)
                {
                    arrival[i] = rand.Next(0, 11); // random arrival time 0–10
                    burst[i] = rand.Next(1, 21);  // random burst time 1–20
                }

                while (completedCount < np)
                {
                    double maxResponseRatio = -1;
                    int selected = -1;

                    for (int i = 0; i < np; i++)
                    {
                        if (!completed[i] && arrival[i] <= currentTime)
                        {
                            double responseRatio = (currentTime - arrival[i] + burst[i]) / burst[i];
                            if (responseRatio > maxResponseRatio)
                            {
                                maxResponseRatio = responseRatio;
                                selected = i;
                            }
                        }
                    }

                    if (selected != -1)
                    {
                        waiting[selected] = currentTime - arrival[selected];
                        currentTime += burst[selected];
                        turnaround[selected] = currentTime - arrival[selected];
                        completed[selected] = true;
                        completedCount++;

                    }
                    else
                    {
                        currentTime++;
                    }
                }

                double avgWaiting = waiting.Sum() / np;
                double avgTurnaround = turnaround.Sum() / np;

                MessageBox.Show("Average waiting time = " + avgWaiting + " sec(s)", "Average Waiting Time", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MessageBox.Show("Average turnaround time = " + avgTurnaround + " sec(s)", "Average Turnaround Time", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        
        public static void fcfsAlgorithm(string userInput)
        {
            int np = Convert.ToInt16(userInput);
            double[] bp = new double[np];
            double[] wtp = new double[np];
            double twt = 0.0, awt;

            DialogResult result = MessageBox.Show("First Come First Serve Scheduling", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                for (int i = 0; i < np; i++)
                {
                    bp[i] = rand.Next(1, 20); // Random burst time between 1–20
                }

                for (int i = 0; i < np; i++)
                {
                    if (i == 0)
                        wtp[i] = 0;
                    else
                        wtp[i] = wtp[i - 1] + bp[i - 1];
                }

                for (int i = 0; i < np; i++)
                {
                    twt += wtp[i];
                }

                awt = twt / np;
                MessageBox.Show("Average waiting time = " + awt + " sec(s)", "Average Waiting Time", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public static void sjfAlgorithm(string userInput)
        {
            int np = Convert.ToInt16(userInput);

            double[] bp = new double[np];
            double[] wtp = new double[np];
            double[] p = new double[np];
            double twt = 0.0, awt; 
            int x, num;
            double temp = 0.0;
            bool found = false;

            DialogResult result = MessageBox.Show("Shortest Job First Scheduling", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                for (num = 0; num <= np - 1; num++)
                {
                    bp[num] = rand.Next(1, 21); // random burst time between 1–20

                }
                for (num = 0; num <= np - 1; num++)
                {
                    p[num] = bp[num];
                }
                for (x = 0; x <= np - 2; x++)
                {
                    for (num = 0; num <= np - 2; num++)
                    {
                        if (p[num] > p[num + 1])
                        {
                            temp = p[num];
                            p[num] = p[num + 1];
                            p[num + 1] = temp;
                        }
                    }
                }
                for (num = 0; num <= np - 1; num++)
                {
                    if (num == 0)
                    {
                        for (x = 0; x <= np - 1; x++)
                        {
                            if (p[num] == bp[x] && found == false)
                            {
                                wtp[num] = 0;

                                bp[x] = 0;
                                found = true;
                            }
                        }
                        found = false;
                    }
                    else
                    {
                        for (x = 0; x <= np - 1; x++)
                        {
                            if (p[num] == bp[x] && found == false)
                            {
                                wtp[num] = wtp[num - 1] + p[num - 1];

                                bp[x] = 0;
                                found = true;
                            }
                        }
                        found = false;
                    }
                }
                for (num = 0; num <= np - 1; num++)
                {
                    twt = twt + wtp[num];
                }
                MessageBox.Show("Average waiting time for " + np + " processes" + " = " + (awt = twt / np) + " sec(s)", "Average waiting time", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public static void priorityAlgorithm(string userInput)
        {
            int np = Convert.ToInt16(userInput);
            double[] bp = new double[np];
            double[] wtp = new double[np];
            int[] p = new int[np];
            int[] sp = new int[np];
            int temp;
            double twt = 0.0, awt;
            bool found = false;

            DialogResult result = MessageBox.Show("Priority Scheduling", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                for (int i = 0; i < np; i++)
                {
                    bp[i] = rand.Next(1, 21);     // burst time 1–20
                    p[i] = rand.Next(1, 6);       // priority 1–5 
                    sp[i] = p[i];

                }

                for (int x = 0; x <= np - 2; x++)
                {
                    for (int i = 0; i <= np - 2; i++)
                    {
                        if (sp[i] > sp[i + 1])
                        {
                            temp = sp[i];
                            sp[i] = sp[i + 1];
                            sp[i + 1] = temp;
                        }
                    }
                }

                int lastScheduled = -1;

                for (int i = 0; i < np; i++)
                {
                    if (i == 0)
                    {
                        for (int x = 0; x < np; x++)
                        {
                            if (sp[i] == p[x] && !found)
                            {
                                wtp[i] = 0;
                                lastScheduled = x;
                                p[x] = -1;
                                found = true;
                            }
                        }
                        found = false;
                    }
                    else
                    {
                        for (int x = 0; x < np; x++)
                        {
                            if (sp[i] == p[x] && !found)
                            {
                                wtp[i] = wtp[i - 1] + bp[lastScheduled];
                                lastScheduled = x;
                                p[x] = -1;
                                found = true;
                            }
                        }
                        found = false;
                    }
                }

                for (int i = 0; i < np; i++)
                {
                    twt += wtp[i];
                }
                awt = twt / np;
                MessageBox.Show("Average waiting time = " + awt + " sec(s)", "Average Waiting Time", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        public static void roundRobinAlgorithm(string userInput)
        {
            int np = Convert.ToInt16(userInput);
            int i, counter = 0;
            double total = 0.0;
            double timeQuantum;
            double waitTime = 0, turnaroundTime = 0;
            double averageWaitTime, averageTurnaroundTime;
            double[] arrivalTime = new double[np];
            double[] burstTime = new double[np];
            double[] temp = new double[np];
            int x = np;

            DialogResult result = MessageBox.Show("Round Robin Scheduling ", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                for (i = 0; i < np; i++)
                {
                    arrivalTime[i] = rand.Next(0, 11); // random arrival time 0–10
                    burstTime[i] = rand.Next(1, 21);  // Random burst time 1–20
                    temp[i] = burstTime[i];

                }

                string timeQuantumInput =
                           Microsoft.VisualBasic.Interaction.InputBox("Enter time quantum: ", "Time Quantum",
                                                              "",
                                                              -1, -1);

                timeQuantum = Convert.ToInt64(timeQuantumInput);
                Helper.QuantumTime = timeQuantumInput;

                for (total = 0, i = 0; x != 0;)
                {
                    if (temp[i] <= timeQuantum && temp[i] > 0)
                    {
                        total += temp[i];
                        temp[i] = 0;
                        counter = 1;
                    }
                    else if (temp[i] > 0)
                    {
                        temp[i] -= timeQuantum;
                        total += timeQuantum;
                    }
                    if (temp[i] == 0 && counter == 1)
                    {
                        x--;



                        turnaroundTime += (total - arrivalTime[i]);
                        waitTime += (total - arrivalTime[i] - burstTime[i]);
                        counter = 0;
                    }
                    if (i == np - 1)
                        i = 0;
                    else if (arrivalTime[i + 1] <= total)
                        i++;
                    else
                        i = 0;
                }

                averageWaitTime = waitTime / np;
                averageTurnaroundTime = turnaroundTime / np;
                MessageBox.Show($"Average wait time = {averageWaitTime} sec(s)", "Average Wait Time", MessageBoxButtons.OK);
                MessageBox.Show($"Average turnaround time = {averageTurnaroundTime} sec(s)", "Average Turnaround Time", MessageBoxButtons.OK);
            }
        }
    }
}

