//
//Class: Form1
//Author: Annie Nguyen
//Date: 2/24/2021
//Lab: Lab 8 Part 1
//Class description: A form that calculates the number of nights and avg/total price when a user makes a 
//reservation
//

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1._1
{
    public partial class frmReservations : Form
    {
        public frmReservations()
        {
            InitializeComponent();
        }
      
        //Method that checks whether user input is valid data
        public bool IsValidData()
        {
            return
            IsPresent(txtArrivalDate, "Arrival Date") &&
            IsDateTime(txtArrivalDate, "Arrival Date") &&
            IsWithinRange(txtArrivalDate, "Arrival Date", DateTime.Today, DateTime.Today.AddYears(5)) &&

            IsPresent(txtDepartureDate, "Departure Date") &&
            IsDateTime(txtDepartureDate, "Departure Date") &&
            IsWithinRange(txtDepartureDate, "Departure Date", DateTime.Today, DateTime.Today.AddYears(5));
            

            
        }

        public bool IsPresent(TextBox textBox, string name)
        {
            if (textBox.Text == "")
            {
                MessageBox.Show(name + " is a required field.", "Entry Error");
                textBox.Focus();
                return false;
            }
            return true;
        }

        //Checks whether date is valid and displays error box
        public bool IsDateTime(TextBox textBox, string name)
        {            
            DateTime validDate = Convert.ToDateTime(textBox.Text);
            if (string.IsNullOrEmpty(textBox.Text) || !DateTime.TryParse(textBox.Text, out validDate))
            {
                MessageBox.Show(name + " is an invalid date.", "Entry Error");
                textBox.Focus();
                return false;

            }
            else
            {
                return true;
            }
        }

        //Checks whether date in within range, up to 5 years, display error messages
        public bool IsWithinRange(TextBox textBox, string name,
            DateTime min, DateTime max)
        {
            DateTime arrival = Convert.ToDateTime(txtArrivalDate.Text);
            DateTime departure = Convert.ToDateTime(txtDepartureDate.Text);
            if (arrival < min || arrival > max)
            {
                MessageBox.Show(arrival + " must be in mm/dd/yyyy format and must be from today to 5 years, from today.");
                txtArrivalDate.Focus();
                return false;
            }
            if (departure < min || departure > max)
            {
                MessageBox.Show(departure + " must be in mm/dd/yyyy format and must be from today to 5 years, from today.");
                txtDepartureDate.Focus();
                return false;
            }

            return true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValidData())
                {
                    //Convert user input to string
                    string arrive = txtArrivalDate.Text;
                    string depart = txtDepartureDate.Text;


                    //Convert string to DateTime and subtract to get total nights, display in textbox
                    DateTime arrivalDate = DateTime.Parse(arrive);
                    DateTime departureDate = DateTime.Parse(depart);
                    TimeSpan nightsStaying = departureDate.Subtract(arrivalDate);
                    int totalNights = nightsStaying.Days;
                    txtNights.Text = Convert.ToString(totalNights);


                    //use while loop to calculate costs for fridays/saturdays and the rest of the week
                    double totalCost = 0;

                    while (arrivalDate.ToShortDateString() != departureDate.ToShortDateString())
                    {
                        if (arrivalDate.DayOfWeek.ToString() == Day.Friday.ToString() || arrivalDate.DayOfWeek.ToString() == Day.Saturday.ToString())
                        {
                            totalCost += 150;
                        }
                        else
                        {
                            totalCost += 120;
                        }
                        arrivalDate = arrivalDate.AddDays(1);

                    }

                    txtTotalPrice.Text = "$" + totalCost;

                    //Calculate average cost by taking totalCost and dividing by total nights
                    double avgCost = totalCost / totalNights;
                    txtAvgPrice.Text = "$" + avgCost;
                }
            }

            //Catch exception
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        //Create load event to have default arrival and departure dates
        private void frmReservations_Load_1(object sender, EventArgs e)
        {
            txtArrivalDate.Text = DateTime.Today.ToString("d");
            txtDepartureDate.Text = DateTime.Today.AddDays(3).ToString("d");
        }
    }
}
