define(['ko','chart'], function (ko,chart) {
     app.consoleModel = function (params) {

         var self = this;
         self.summary = {
             Q: ko.observable(),
             Start_of_Quarter: ko.observable(),
             Successful_Phishing_Attempts: ko.observable(),
             Total_Attachment_Clicks: ko.observable(),
             Total_Campaigns: ko.observable(),
             Total_Emails_Sent: ko.observable(),
             Total_Link_Clicks: ko.observable()
         }
         self.repeatOffenders = ko.observableArray();
         self.monthlyClicks = {
             type: 'bar',
             data: ko.observable({
                 labels: ['Red', 'Blue', 'Yellow', 'Green', 'Purple', 'Orange'],
                 datasets: [{
                     label: '# of Votes',
                     data: [12, 19, 3, 5, 2, 3],
                     backgroundColor: [
                         'rgba(255, 99, 132, 0.2)',
                         'rgba(54, 162, 235, 0.2)',
                         'rgba(255, 206, 86, 0.2)',
                         'rgba(75, 192, 192, 0.2)',
                         'rgba(153, 102, 255, 0.2)',
                         'rgba(255, 159, 64, 0.2)'
                     ],
                     borderColor: [
                         'rgba(255, 99, 132, 1)',
                         'rgba(54, 162, 235, 1)',
                         'rgba(255, 206, 86, 1)',
                         'rgba(75, 192, 192, 1)',
                         'rgba(153, 102, 255, 1)',
                         'rgba(255, 159, 64, 1)'
                     ],
                     borderWidth: 1
                 }]
             }),
             options: ko.observable( {
                 scales: {
                     yAxes: [{
                         ticks: {
                             beginAtZero: true
                         }
                     }]
                 }
             })
         }
         self.getSummary = function () {
            
             app.api('get', "/Report/ConsoleSummary", null, function (data) {
                 console.log(data)
                 with (self.summary) {
                     Q(data[0].Q);
                     Start_of_Quarter(app.dt(data[0].Start_of_Quarter));
                     Successful_Phishing_Attempts(data[0].Successful_Phishing_Attempts);
                     Total_Attachment_Clicks(data[0].Total_Attachment_Clicks);
                     Total_Campaigns(data[0].Total_Campaigns);
                     Total_Emails_Sent(data[0].Total_Emails_Sent);
                     Total_Link_Clicks(data[0].Total_Link_Clicks);
                 }

             });
         }
         self.getSummary();
         self.getRepeatOffenders = function () {
             self.repeatOffenders.removeAll();
             app.api('get', "/Report/RepeatOffenders", null, function (data) {
                 self.repeatOffenders(data)
                 console.log(data)
             });
         }
         self.getRepeatOffenders();


    };
    var defaults = function (params) {
        var self = this;
        params = params ?? {}
        //self.options = params.options ?? {};
        //self.data = params.data ?? {};
        return self;
    }
    return app.consoleModel;
});