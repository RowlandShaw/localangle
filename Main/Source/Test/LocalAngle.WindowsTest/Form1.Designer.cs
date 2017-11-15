namespace LocalAngle.WindowsTest
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.specialEventBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.eventIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.distanceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.eventUriDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.venueNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lastModifiedDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.latitudeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.longitudeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.locationDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.startTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.endTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.publishStatusDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tagsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ticketUriDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.postcodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.specialEventBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.eventIdDataGridViewTextBoxColumn,
            this.nameDataGridViewTextBoxColumn,
            this.descriptionDataGridViewTextBoxColumn,
            this.distanceDataGridViewTextBoxColumn,
            this.eventUriDataGridViewTextBoxColumn,
            this.venueNameDataGridViewTextBoxColumn,
            this.lastModifiedDataGridViewTextBoxColumn,
            this.latitudeDataGridViewTextBoxColumn,
            this.longitudeDataGridViewTextBoxColumn,
            this.locationDataGridViewTextBoxColumn,
            this.startTimeDataGridViewTextBoxColumn,
            this.endTimeDataGridViewTextBoxColumn,
            this.publishStatusDataGridViewTextBoxColumn,
            this.tagsDataGridViewTextBoxColumn,
            this.ticketUriDataGridViewTextBoxColumn,
            this.postcodeDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.specialEventBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 33;
            this.dataGridView1.Size = new System.Drawing.Size(1597, 795);
            this.dataGridView1.TabIndex = 0;
            // 
            // specialEventBindingSource
            // 
            this.specialEventBindingSource.DataSource = typeof(LocalAngle.Events.SpecialEvent);
            // 
            // eventIdDataGridViewTextBoxColumn
            // 
            this.eventIdDataGridViewTextBoxColumn.DataPropertyName = "EventId";
            this.eventIdDataGridViewTextBoxColumn.HeaderText = "EventId";
            this.eventIdDataGridViewTextBoxColumn.Name = "eventIdDataGridViewTextBoxColumn";
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Event name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            // 
            // descriptionDataGridViewTextBoxColumn
            // 
            this.descriptionDataGridViewTextBoxColumn.DataPropertyName = "Description";
            this.descriptionDataGridViewTextBoxColumn.HeaderText = "Description";
            this.descriptionDataGridViewTextBoxColumn.Name = "descriptionDataGridViewTextBoxColumn";
            // 
            // distanceDataGridViewTextBoxColumn
            // 
            this.distanceDataGridViewTextBoxColumn.DataPropertyName = "Distance";
            this.distanceDataGridViewTextBoxColumn.HeaderText = "Distance";
            this.distanceDataGridViewTextBoxColumn.Name = "distanceDataGridViewTextBoxColumn";
            // 
            // eventUriDataGridViewTextBoxColumn
            // 
            this.eventUriDataGridViewTextBoxColumn.DataPropertyName = "EventUri";
            this.eventUriDataGridViewTextBoxColumn.HeaderText = "EventUri";
            this.eventUriDataGridViewTextBoxColumn.Name = "eventUriDataGridViewTextBoxColumn";
            this.eventUriDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // venueNameDataGridViewTextBoxColumn
            // 
            this.venueNameDataGridViewTextBoxColumn.DataPropertyName = "VenueName";
            this.venueNameDataGridViewTextBoxColumn.HeaderText = "Venue";
            this.venueNameDataGridViewTextBoxColumn.Name = "venueNameDataGridViewTextBoxColumn";
            // 
            // lastModifiedDataGridViewTextBoxColumn
            // 
            this.lastModifiedDataGridViewTextBoxColumn.DataPropertyName = "LastModified";
            this.lastModifiedDataGridViewTextBoxColumn.HeaderText = "LastModified";
            this.lastModifiedDataGridViewTextBoxColumn.Name = "lastModifiedDataGridViewTextBoxColumn";
            // 
            // latitudeDataGridViewTextBoxColumn
            // 
            this.latitudeDataGridViewTextBoxColumn.DataPropertyName = "Latitude";
            this.latitudeDataGridViewTextBoxColumn.HeaderText = "Latitude";
            this.latitudeDataGridViewTextBoxColumn.Name = "latitudeDataGridViewTextBoxColumn";
            // 
            // longitudeDataGridViewTextBoxColumn
            // 
            this.longitudeDataGridViewTextBoxColumn.DataPropertyName = "Longitude";
            this.longitudeDataGridViewTextBoxColumn.HeaderText = "Longitude";
            this.longitudeDataGridViewTextBoxColumn.Name = "longitudeDataGridViewTextBoxColumn";
            // 
            // locationDataGridViewTextBoxColumn
            // 
            this.locationDataGridViewTextBoxColumn.DataPropertyName = "Location";
            this.locationDataGridViewTextBoxColumn.HeaderText = "Location";
            this.locationDataGridViewTextBoxColumn.Name = "locationDataGridViewTextBoxColumn";
            // 
            // startTimeDataGridViewTextBoxColumn
            // 
            this.startTimeDataGridViewTextBoxColumn.DataPropertyName = "StartTime";
            this.startTimeDataGridViewTextBoxColumn.HeaderText = "Start";
            this.startTimeDataGridViewTextBoxColumn.Name = "startTimeDataGridViewTextBoxColumn";
            // 
            // endTimeDataGridViewTextBoxColumn
            // 
            this.endTimeDataGridViewTextBoxColumn.DataPropertyName = "EndTime";
            this.endTimeDataGridViewTextBoxColumn.HeaderText = "End";
            this.endTimeDataGridViewTextBoxColumn.Name = "endTimeDataGridViewTextBoxColumn";
            // 
            // publishStatusDataGridViewTextBoxColumn
            // 
            this.publishStatusDataGridViewTextBoxColumn.DataPropertyName = "PublishStatus";
            this.publishStatusDataGridViewTextBoxColumn.HeaderText = "PublishStatus";
            this.publishStatusDataGridViewTextBoxColumn.Name = "publishStatusDataGridViewTextBoxColumn";
            // 
            // tagsDataGridViewTextBoxColumn
            // 
            this.tagsDataGridViewTextBoxColumn.DataPropertyName = "Tags";
            this.tagsDataGridViewTextBoxColumn.HeaderText = "Tags";
            this.tagsDataGridViewTextBoxColumn.Name = "tagsDataGridViewTextBoxColumn";
            // 
            // ticketUriDataGridViewTextBoxColumn
            // 
            this.ticketUriDataGridViewTextBoxColumn.DataPropertyName = "TicketUri";
            this.ticketUriDataGridViewTextBoxColumn.HeaderText = "Online ticketing";
            this.ticketUriDataGridViewTextBoxColumn.Name = "ticketUriDataGridViewTextBoxColumn";
            // 
            // postcodeDataGridViewTextBoxColumn
            // 
            this.postcodeDataGridViewTextBoxColumn.DataPropertyName = "Postcode";
            this.postcodeDataGridViewTextBoxColumn.HeaderText = "Venue postal code";
            this.postcodeDataGridViewTextBoxColumn.Name = "postcodeDataGridViewTextBoxColumn";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.AutoSize = true;
            this.button1.Location = new System.Drawing.Point(12, 813);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(178, 35);
            this.button1.TabIndex = 1;
            this.button1.Text = "Load Something";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1621, 860);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.specialEventBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn eventIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn distanceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn eventUriDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn venueNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastModifiedDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn latitudeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn longitudeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn locationDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn startTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn endTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn publishStatusDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tagsDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ticketUriDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn postcodeDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource specialEventBindingSource;
        private System.Windows.Forms.Button button1;
    }
}

