
var singnalr_hub_module = function () {
  var myName;

  var transport = signalR.TransportType.WebSockets;
  var connection = new signalR.HubConnection('/goatingChat');
  //new signalR.HubConnection('http://' + document.location.host + '/goatingChat', { transport: transport });  

  function init() {
    // page controls
    $('#message-box').on('keyup', function (e) {
      if (e.keyCode == 13) {
        var msg = $('#message-box').val();
        if (msg !== "") {
          connection.invoke('SendMessage', msg, getGroupName())
          $('#message-box').val('');
          //addToChat("You: " + msg)
          $('#message-box').focus();
        }
      }
    });

    $(".groups-list li a").on("click", function (e) {
      var group = $(this).attr("data-group-name");
      joinGroup(group);
      e.preventDefault();
    });

    // bind incoming messages to functions
    connection.on('connected', function (msg) { addRemoveMemberToChat(msg); });
    connection.on('chatName', function (msg) { $('#main-chat-title').text(msg); });
    connection.on('receiveMessage', function (msg, group) { addToChat(msg, group); });
    connection.on('disconnected', function (msg) { addRemoveMemberToChat(msg); });
    connection.on('myName', function (msg) { myName = msg; });

    connection.start().then(function () {
      connection.invoke('Connected');
      connection.invoke('ChatName');
      connection.invoke('MyName')
    });
  }

  // message, class
  function addToChat(msg, group) {
    /// <summary>Adds a received message to the active chat.</summary>  
    /// <param name="msg" type="string">The message to add.</param>
    if ($.trim(msg) !== "" && group == getGroupName()) {
      $('#chat-window').append('<div class="message-received"><div class="message-received-text"><span><i class="fa fa-user-secret fa-5" aria-hidden="true"></i></span><span>' + msg + '</span></div><div class="message-time">' + getDate() + '</div></div>');
      site_module.updateScroll();
    }
  }

  function addRemoveMemberToChat(msg) {
    /// <summary>Adds a member added or removed to the active chat.</summary>  
    /// <param name="msg" type="string">The message to add.</param>  
    $('#chat-window').append('<div class="message-new-member">' + msg + '</div>');
  }

  function getDate() {
    /// <summary>Gets a hh:mm:ss time stamp</summary>
    /// <returns type="string">Time stamp</returns>  
    var today = new Date(Date.now());
    var time = today.getHours() + ':' + today.getMinutes() + ':' + today.getSeconds();
    return time;
  }

  function joinGroup(groupName) {
    $("#chatGroup").text(groupName);
    connection.invoke('JoinGroup', groupName);
  }

  function getGroupName() {
    return $("#chatGroup").text();
  }

  window.onbeforeunload = function (e) {
    connection.invoke('Disconnect');
  };

  return {
    init: init
  }
}();

$(document).ready(function () {
  singnalr_hub_module.init();
});