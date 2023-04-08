//注册事件
function registerNotifyClientMethods(hub) {
    hub.client.broadcastChanged = function () {
        //console.log('broadcastChanged',name);
        hub.server.getNotification();

    };
    hub.client.broadcastNotif = function (totalNotif) {
        //console.log('broadcastNotif', totalNotif);
        $this = $("#activity > .badge");
        const num = parseInt($this.text().trim());
        if (num !== totalNotif) {
            if (totalNotif > 0) {

                $this.addClass("bg-color-red bounceIn animated");
                $this.text(totalNotif);
            } else {
                $this.removeClassPrefix("bg-color-");
                $this.text("0");
            }
            loadnotifdata();
        }
        $this = null;
    };
}
function registerNotifyEvent(hub) {
    hub.server.connect();
  //hub.server.getNotification();
}
  
  
function loadnotifdata() {
    const curruser = $('#currentuser').val();
    const url = `/Notifications/GetNotifyData?userName=${curruser}`;
    let group0 = 0;
    let group1 = 0;
    let group2 = 0;
    let group3 = 0;
    $.get(url).done(res => {
        if (res.data) {
            $this = $('.ajax-notifications').find('ul[class="notification-body"]');
            $this.empty();
            res.data.forEach(item => {
                switch (item.Group) {
                    case 0:
                        group0 += 1;
                        break;
                    case 1:
                        group1 += 1;
                        break;
                    case 2:
                        group2 += 1;
                        break;
                    case 3:
                        group3 += 1;
                        break;
                }
                const fa = faiconformat(item.Group);
                const from = fromformat(item.Group, item.From);
                const time = timeago.format(moment(item.Created).format('YYYY-MM-DD HH:mm:ss'), 'zh_CN');
                const li = `<li>
                       <span class="unread">
                         <a href="javascript:void(0);" class="msg">
                           <em class="air air-top-left margin-top-5" width="40" height="40">
                             <i class="fa ${fa} fa-fw fa-2x"></i>
                           </em>
                            <span class="from">${from} <i class="icon-paperclip"></i></span>
                  <time>${time}</time>
                  <span class="subject">${item.Content}</span>
                         </a>
                       </span>
                      </li>`;
                $this.append(li);
            });


            const labels = $('.ajax-dropdown > .btn-group-justified[data-toggle="buttons"] > label');
            $(labels[0]).contents().last()[0].textContent = `系统推送(${group0}) `;
            $(labels[1]).contents().last()[0].textContent = `业务推送(${group1}) `;
            $(labels[2]).contents().last()[0].textContent = `接口推送(${group2}) `;
            $(labels[3]).contents().last()[0].textContent = `人工推送(${group3}) `;
             
        }
    });
}
  function faiconformat(group) {
    switch (group) {
      case 0:
        return 'fa-windows';
      case 1:
        return 'fa-bell-o';
      case 2:
        return 'fa-plug';
      case 3:
        return 'fa-user';
      default:
        return 'fa-bolt';
    }
  }
  function fromformat(group,from) {
    switch (group) {
      case 0:
        return '系统推送';
      case 1:
        return '业务推送';
      case 2:
        return '接口推送';
      case 3:
        return from;
      default:
        return 'fa-bolt';
    }
  }
