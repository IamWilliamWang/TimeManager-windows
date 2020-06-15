# 关机助手 v4.6

## 功能概述：
基础功能支持
> * 开关机时间记录：    
便捷的记录每次开机、关机时间
> * 定时关机：    
可设定想要关机电脑的时间
> * 定时重启：    
可设定想要重启电脑的时间
> * 定时休眠：    
可设定想要休眠电脑的时间
> * 定时睡眠：    
可设定想要睡眠电脑的时间

强大的时间管理系统可以让你轻松对储存的时间记录进行
> * 显示    
可选择显示所有记录或者显示最近的记录
> * 编辑    
在窗格中随性编辑，之后点击提交即可
> * 添加    
按照你的意愿存入数据
> * 删除    
按照你的意愿任意删除一条或多条或全部数据
> * 备份    
支持backup格式与rar格式（自带加密）的一键备份
> * 还原    
支持backup格式与rar格式的一键还原

方法多样、满足各用户的使用习惯。

除了基本的增删改查，还添加了多种多样的高级功能，如：
> * 计算每天的开机次数
> * 计算每月的开机次数
> * 计算每月上机累计时长
> * 计算开关机的时间分布

并能将这些结果使用`数据可视化`功能使用图和表格展现出来，给用户最直观的展示。

高级功能还有
> * 日志管理    
记录每次操作日志，操作有异常可以使用该功能查看
> * 注释管理    
对已有的某一次数据附加或修改解释性说明，解释性文件等等
> * 系统休眠管理    
部分用户尚未开启系统休眠可使用该功能一键开启
> * 缓存管理    
可对本程序的缓存进行添加、修改、删除、合并、清理、另存为、移动、备份等操作

日志管理内可以查看每次使用软件的记录以及每次执行数据库操作所用的时长，下方得出平均时长，如果检测到执行时间普遍过长，程序会弹出相应的改善建议；注释管理器中用户可以对已有的记录进行备注，并可以查看以前添加的所有备注内容和添加日期，使得记录的内容丰富多样起来。

为了程序的健壮性，特别增加了数据库锁防止因为操作过频繁而产生程序崩溃。即使程序因为未知原因产生崩溃，软件也内置了异常处理功能，当异常情况发生的时候，程序可以进行有效的异常报告与异常修复，使得程序在修复后可以重新在正常的状态下进行工作。

另外还为程序员用户设置了命令行选项，使得不打开窗体直接向窗体程序传参也能完成部分简单功能。这些简单功能包括：

|选项|完整选项|含义|示例|
|---|-|:-:|-|
|-s [sec/min]s/m |--shutdown_seconds [sec/min]s/m |倒计时关机(秒)                                     |-s 60s or -s 1m
|-d [sec/min]s/m |--shutdown_delay [sec/min]s/m   |记录被delay后的关机时间                            |-d 30s or -d 0.5m 
|-c [string]     |--comment [string]              |执行成功后弹出的字符串(换行请使用\\n表达)                     |-s 2.5m -c 150秒后将关机
|-a              |--cancel_all                    |销毁所有倒计时                                     |-a
|-k              |--start                         |记录当前的开机时间                                 |-k
|-h              |--hibernate                     |休眠电脑(记录关机和下次开机时间)                   |-h
|-sleep          |--sleep                         |睡眠电脑(记录关机和下次开机时间)                   |-sleep
|-db [dbFilename]|--database_filename [dbFilename]|设定数据库文件名(不使用-dc会自动检测对应的缓存文件)|-db D:\\database.mdf
|-ca [cachename] |--cache [cachename]             |设定数据库缓存文件名                               |-ca D:\\database.cache
|-dc             |--disable_cache                 |强制禁止使用缓存                                   |-dc
|-offline        |--offline                       |离线模式，不记录任何时间                           |-offline
|-sc             |--show_cache                    |显示缓存文件内容（可指定缓存文件）                 |-sc -ca my_cache.cache
|-del            |--delete_cache                  |删除缓存文件（可指定缓存文件）                     |-del -ca my_cache.cache

注：
* -c -a -k -dc指令不会引起系统的关机或者休眠
* 由于4.0版本开始，优先插入数据到缓存而不是数据库，如果想直接对数据库进行操作，请使用-dc
* 选项中可使用/代替-，如/s 1m

最后顺便提到，主界面在开发1.x版本时内置了多种隐藏功能，用户可通过以下操作进行探索：
> * 主界面右击设置外观等行为
> * 主界面双击激活开发者动作
> * 确定键右击注册、销毁定时关机行为（每次开机启用该关机倒计时，实现防沉迷功能）。

## 免安装版软件下载
[点击这里下载最新版本](https://github.com/IamWilliamWang/TimeManager-windows/releases/download/v4.6/TimeManager.exe)（需要预装.NET framework 4.6.1及以上版本）

[点击这里下载3.12稳定版](https://github.com/IamWilliamWang/TimeManager-windows/releases/download/v4.0.0/TimeManager_v3.12.exe)

## 更新内容
当前版本号：**4.6**(主程序版本) + **2.1**(命令行版本)

### 主程序更新日志：
|版本号|新功能|调整与修改|修复问题|
|:-:|:-|:-|:-|
|[4.6](https://github.com/IamWilliamWang/TimeManager-windows/releases/download/v4.6/TimeManager.exe)|主界面支持了取消休眠/睡眠，取消的同时会杀相同程序的后台。配置文件增加了默认的模式选择。取消后增加BallonTip交互。定时休眠增加了提前10、3、1分钟提醒BallonTip|主管理窗口默认显示15条改为20条。主管理窗口菜单栏选项、主界面文字、配置文件显示微调。主界面的指定时间关机功能改为指定时间，第二个确定只作为委托|修复了日志管理器自动备份报错问题
|[4.5.4](https://github.com/IamWilliamWang/TimeManager-windows/releases/download/v4.6/TimeManager_v4.5.4.exe)|检测没有配置文件会生成范例|将配置格式显示移入配置窗体|修正了范例格式和ConfigManager内部可能导致的所有加载错误
|[4.5.3](https://github.com/IamWilliamWang/TimeManager-windows/releases/download/v4.5.3/TimeManager.exe)|README添加每个版本的下载链接。主界面右菜单添加隐藏右下角徽标按钮。配置文件添加了透明度设置、自动隐藏于任务栏、自动隐藏右下角徽标并同步至显示配置文件、配置文件格式。单击右下角徽标可以显示窗口|README大整理，调整结构，将更新日志改成表格更加直观。显示配置文件的说明文字修改|修复notifyIcon的残留和休眠卡死问题
|[4.5.2](https://github.com/IamWilliamWang/TimeManager-windows/releases/download/v4.5.2/TimeManager.exe)|增加了右下角徽标和徽标的右键菜单，定时休眠和睡眠会有通知提示，也可以取消定时||修复了软件定时休/睡眠情况下进程残留的问题
|[4.5.1](https://github.com/IamWilliamWang/TimeManager-windows/releases/download/v4.5.1/TimeManager.exe)|||修复了缓存管理的切换模式时标题混乱、点击合并后仍然提示需保存修改的问题。修复了主窗口因行数过多导致行号被遮挡的问题
|[4.5](https://github.com/IamWilliamWang/TimeManager-windows/releases/download/v4.5/TimeManager.exe)|缓存管理器升级内核并重构，支持自动备份，增添了显示模式菜单以及新的直观模式。主窗体新增显示后n条数据功能。命令行版本更新至2.0.2|安装插件无需重启程序，菜单栏文字调整。睡/休眠时不打勾不会记录开机。显示配置文件独立出窗体|修复了修改检测的问题
|4.4.3|命令行cache支持任意文件名。缓存管理器可以取消关闭|命令行写缓存会跟随程序路径。简化内容修改的判断过程
|[4.4.2](https://github.com/IamWilliamWang/TimeManager-windows/releases/download/v4.4.2/TimeManager.exe)|关机倒计时可切换分钟或小时||修复暗黑模式显示缺漏
|[4.4.1](https://github.com/IamWilliamWang/TimeManager-windows/releases/download/v4.4.1/TimeManager.exe)|增加配置文件显示和帮助|废弃并删除“导入所有数据(旧版)”功能
|[4.4](https://github.com/IamWilliamWang/TimeManager-windows/releases/download/v4.4/TimeManager.exe)||缓存管理器改进，隐藏Cache内Sql细节，只清晰明了的显示开机时间和关机时间部分
|[4.3.5](https://github.com/IamWilliamWang/TimeManager-windows/releases/download/v4.3.5/TimeManager.exe)|引入完善的BackupCreater|修改了删除缓存备份的时间局限于缓存管理器内
|[4.3.4](https://github.com/IamWilliamWang/TimeManager-windows/releases/download/v4.3.4/TimeManager.exe)|打开缓存管理器时立即备份，主界面关闭时自动清理备份||合并缓存与加载数据循环调用问题已解决
|4.3.3|增加缓存备份模块，可以在执行时丢失缓存文件后还能找回
|4.3.2|增加加载配置功能。支持了自动填充缓存合并的源、目标文件名、自动合并和主界面自动Dark Mode
|[4.3](https://github.com/IamWilliamWang/TimeManager-windows/releases/download/v4.3/TimeManager.exe)||废除旧加密算法，删最后一条记录根据开机时间进行判断。修改延缓模式缓存格式为了能正常提交，缓存修改后进行其他操作会有保存提示|修复了缓存提交失败时数据库插入错误数据问题
|[4.2.5](https://github.com/IamWilliamWang/TimeManager-windows/releases/download/v4.2.5/TimeManager.exe)|添加了导入数据库向后兼容的选项|删除了原始版本的导入导出功能。新版的导入功能不会修改备份文件|修复了解压一直失败问题
|4.2.4|||修复了还原数据库一直遗留的致命错误
|[4.2.3](https://github.com/IamWilliamWang/TimeManager-windows/releases/download/v4.2.3/TimeManager.exe)|记录开机时间、注册关机事件适配终端2.0接口并支持utf-8脚本|应用新的数据库加密算法
|4.2.1||更新注册关机时间调用接口，加密解密算法更新为更安全的算法
|[4.2](https://github.com/IamWilliamWang/TimeManager-windows/releases/download/v4.2/TimeManager.exe)|主界面支持dark mode。命令行版本大更新至2.0.1|历史版本号简化，去除三级.0。主界面删除最小化按钮，布局更宽阔|修复延缓功能不支持缓存的问题
|4.1.7|缓存管理添加ScrollBar|数据可视化大小可调|修复了InputBox设定默认字串情况下自动清除文字的问题
|4.1.6||重新整理SqlConnectionAgency内部函数，重命名为DatabaseAgency，并增加其使用率
|[4.1.5](https://github.com/IamWilliamWang/TimeManager-windows/releases/download/v4.1.5/TimeManager.exe)|为了降低不必要的操作，补丁程序所有功能成功合并至缓存管理器。补丁程序停止开发|清理缓存功能提供空文件、文件不存在提示。找不到缓存文件不再弹窗提示
|4.1.4|当安全模式开启时在高级选项中有停用安全模式按钮，按下可关闭安全模式状态|安全模式降耦。提升安全模式切换执行速度|修复了任何在数据库打开后仍能开启安全模式的漏洞
|4.1.3|CacheUtil增添自定义缓存文件方法|集成补丁程序1.3版本
|4.1.2|将补丁程序1.2版本集成入主界面右键菜单，并改名为缓存生成与合并|将缓存管理移至主界面右键菜单，重命名为缓存编辑。将缓存生成与合并、缓存编辑合并为缓存管理。在主界面进行缓存管理解决了每次都要进行安全模式才能管理缓存的尴尬
|4.1.1|InputBox添加DedaultText和提示功能|优化InputBox界面|修复由默认字段产生的报错问题
|[4.1](https://github.com/IamWilliamWang/TimeManager-windows/releases/download/v4.1/TimeManager.exe)|新的InputBox，更美观。当数据管理窗口大小保持不变时，数据展示可自动拓宽/缩小窗体宽度|全体代码重构，过时代码全去除。注册关机事件增加失败提示。数据管理任务栏提示居右。休眠窗口不重要已移至Util。精准查找防错处理|获取管理员权限功能修复，重启后不会有重复提示
|4.0.5||细节措辞修改。缓存管理在安全模式下不可提交到数据库
|[4.0.4](https://github.com/IamWilliamWang/TimeManager-windows/releases/download/v4.0.5/TimeManager_v4.0.4.exe)|主窗口行号宽度、界面宽度自适应|隐秘功能去除。连接数据库后离线功能禁用
|4.0.3|显示功能支持精准查找显示|缓存显示编辑部分代码重构，所有相关功能可视化并移到高级功能选项卡
|4.0.2|增添缓存功能异常报警||紧急修复无法插入开机记录的问题
|[4.0.1](https://github.com/IamWilliamWang/TimeManager-windows/releases/download/v4.0.5/TimeManager_v4.0.1.exe)|安全模式支持可逆操作。增添编辑缓存功能。管理主窗口支持拖拽大小，并支持在任意一次刷新后根据内容调整各列的宽度功能。外链数据库快捷功能加入防误拖拽功能。数据可视化增快速度|安全模式加固，完全阻断连接保证离线状态。改善缓存查看功能。重整管理主界面的管理员选项。防重复启动功能修改。注释管理器去掉HeaderCell的干扰索引号|修复了不同版本下数据库插入格式不一导致崩溃，修复了系统时钟格式不同导致的首页时间显示异常，数据可视化画图日期混乱问题。修复了由于不同的系统时钟格式，数据表刷新后每列宽度变乱问题
|[4.0](https://github.com/IamWilliamWang/TimeManager-windows/releases/download/v4.0.0/TimeManager.exe)|休眠和待机功能首度支持定时执行。采用缓存技术使主窗口操作速度普遍提升95%，并能在管理主窗口显示清除缓存的进度、查看缓存文件。数据库首次实现了所有系统的适配，所有用户可以享受同样的体验。数据表格都可以显示行索引了
|[3.12](https://github.com/IamWilliamWang/TimeManager-windows/releases/download/v4.0.0/TimeManager_v3.12.exe)|程序会根据内容自动调整到最佳的显示状态，不会有内容显示不全|数据显示方式不使用默认格式，更加美观自然|修复了注释管理器刷新后界面变乱的问题
|3.11.5|在主界面右击菜单增加外链数据库按钮|自定义数据库改为外链数据库，整理了主界面右击菜单内容。调整异常处理窗体的逻辑，显得不那么混乱|修复了部分表格宽度过短导致显示不全的问题
|3.11.4|在主界面添加了临时禁止开机记录时间功能，使用sqlite数据库加速访问速度(beta)|自定义sql脚本运行交互完善(beta)
|3.11.3|主界面添加睡眠功能|代码优化|内部流程统配修复了用户名不一样产生的错误
|[3.11.2](https://github.com/IamWilliamWang/TimeManager-windows/releases/download/v4.0.0/TimeManager_v3.11.2.exe)|提供自定义sql脚本的运行功能(beta)|重启时不再一律不记录下次开机时间而是根据用户勾选而定|修复异常窗口退出后程序还在执行的问题
|[3.11.1](https://github.com/IamWilliamWang/TimeManager-windows/releases/download/v4.0.0/TimeManager_v3.11.1.exe)|注释管理器支持最大化|管理窗口更新完数据自动刷新显示的数据
|[3.11](https://github.com/IamWilliamWang/TimeManager-windows/releases/download/v4.0.0/TimeManager_v3.11.exe)|主界面添加获得管理员权限功能、添加离线模式（不使用数据库）|代码优化
|3.10.9|管理员选项可以设置系统休眠状态
|3.10.8|重启功能自动取消记录关机时间，在管理界面也可以使用终端版本的关机助手
|[3.10.7](https://github.com/IamWilliamWang/TimeManager-windows/releases/download/v4.0.0/TimeManager_v3.10.7.exe)|备份文件进行特殊加密，在主页可以释放数据库
|3.10.6|支持使用RAR备份文件还原数据库，添加Copyright
|3.10.5||分析界面表格部分中文化，主界面过渡效果更自然
|[3.10.4](https://github.com/IamWilliamWang/TimeManager-windows/releases/download/v3.10.4/TimeManager.exe)|异常处理窗口可快捷退出|主界面菜单栏调整，界面提示修改，删除不必要的部分
|3.10.3|对于硬盘速度较慢的用户，在日志管理内提供合理建议以提高软件使用速度
|[3.10.2](https://github.com/IamWilliamWang/TimeManager-windows/releases/download/v3.10.4/TimeManager_v3.10.2.exe)||分析界面切换到分析时间分布时不再重置图像为柱状图|修复了初始化数据库不可用的问题
|3.10.1||对注释管理器的确认修改功能作出相应的调整|注释管理器修复了Unicode显示异常的问题，不再对非中文字符进行转化
|3.10|注释管理器使用Unicode编码技术支持中文输入|数据库相关代码重构
|3.9.7|将mdf数据库文件拖入主页面可以载入该数据库而不是载入默认数据库|注释管理器会判断序号是否合法
|3.9.6|对主界面添加拓展功能动画||注释管理器的修改功能问题修复
|3.9.5|注释管理器添加选项卡清爽界面|优化界面细节、注释管理器的显示信息
|3.9.4|支持了使用winrar进行数据库备份的功能
|3.9.3|首次在主管理窗口中添加“添加一条关机记录”|错误弹窗能显示更全面的信息
|3.9.2|增添了针对报错框的调试功能（强制报错）||修复了选择休眠状态下滚动鼠标报错的问题
|3.9.1|管理员选项中增添查看数据库连接文件和状态的选项|主界面快捷栏调整，删除全部功能完善
|3.9|增加了注释管理器，可以对单条数据进行添加和修改注释|数据管理界面调整
|[3.8.1](https://github.com/IamWilliamWang/TimeManager-windows/releases/download/v3.10.4/TimeManager_v3.8.1.exe)|手动添加关机时间后，选择填充空处可以进行填充时长栏|改进异常处理
|[3.8](https://github.com/IamWilliamWang/TimeManager-windows/releases/download/v3.10.4/TimeManager_v3.8.0.exe)||主界面隐藏不常用功能，并将退出改为显示拓展功能。日志窗口设置为不可变长宽。重启时不再调用开机启动程序，节省系统资源也省去需要手动删除记录的操作
|3.7.3|当执行需要管理员权限的操作，程序可以询问后自己获取管理员权限
|3.7.2||数据可视化默认图形改变，优化开发者模式
|[3.7.1](https://github.com/IamWilliamWang/TimeManager-windows/releases/download/v3.7.1/TimeManager.exe)|||修复日志管理中可能存在数据量过大而产生的数值溢出的问题
|3.7|添加了欢迎页面与自动初始化的功能|将数据库文件导向至软件所在文件夹，数据管理页面微调
|[3.6.2](https://github.com/IamWilliamWang/TimeManager-windows/releases/download/v3.7.1/TimeManager_v3.6.2.exe)|支持数据管理界面最大化|查询功能由查询10条改为15条
|[3.6](https://github.com/IamWilliamWang/TimeManager-windows/releases/download/v3.7.1/TimeManager_v3.6.0.exe)|数据可视化界面添加开关机时间分析|数据可视化界面微调
|3.5.5|增加延缓模式，可以储存真正关机的时间
|3.5.4|添加删除任意条记录的功能
|3.5.2||查询功能微调，报错窗口微调
|[3.5.1](https://github.com/IamWilliamWang/TimeManager-windows/releases/download/v3.7.1/TimeManager_v3.5.1.exe)||正式改名为关机助手|修复因卡顿产生的错误
|3.5|增加了休眠模式|主页可以方便的插入开机时间
|3.3.5|增加了报告错误的窗体，便于检查与反馈错误
|3.3.1||增加数据库稳定性
|3.3|添加了日志管理
|3.2.5|启用数据库日志功能
|[3.2](https://github.com/IamWilliamWang/TimeManager-windows/releases/download/v3.7.1/TimeManager_v3.2.0.exe)|使用多线程实现了软件零卡顿，实现了终端模式
|3.1.6|添加了开发者模式，可以直接使用SQL语句管理数据库（仅限开发者用户）
|[3.1.4](https://github.com/IamWilliamWang/TimeManager-windows/releases/download/v3.1.4/TimeManager.exe)|备份还原功能完美实现|性能优化
|[3.0](https://github.com/IamWilliamWang/TimeManager-windows/releases/download/v3.0/TimeManager.exe)|统计窗体全面升级，软件全面翻新
|2.8|标题显示时间|修改管理器启动按钮
|2.7|支持滚轮操作，支持在面板内更新数据
|2.6|可以统计出每个月使用的累计时间|调整数据库管理的界面与菜单
|[2.3](https://github.com/IamWilliamWang/TimeManager-windows/releases/download/v2.3/TimeManager.exe)|支持数据全部导出到excel功能
|[2.2](https://github.com/IamWilliamWang/TimeManager-windows/releases/download/v2.2/TimeManager.exe)|大大优化速度||开机程序修复
|[2.1](https://github.com/IamWilliamWang/TimeManager-windows/releases/download/v2.1/TimeManager.exe)|关机可以计算时长并记录，计算当天次数||修复了时间插入失败的问题
|[2.0](https://github.com/IamWilliamWang/TimeManager-windows/releases/download/v2.1/TimeManager_v2.0.exe)|添加记录开关机的功能。新建全新的管理界面
|[1.4](https://github.com/IamWilliamWang/TimeManager-windows/releases/download/v1.4/TimeManager.exe)||改善弹窗外观|修复IME模式禁止出现中文
|1.3.3|Enter键可以代替确认按钮，q键取消任务，Esc键可以退出|取消防误触功能
|[1.3.2](https://github.com/IamWilliamWang/TimeManager-windows/releases/download/v1.3.2/TimeManager.exe)|增加了开发者模式|将【帮助】移至主界面
|1.3.1|菜单右菜单增加【帮助】||修复1.3的错误
|[1.3](https://github.com/IamWilliamWang/TimeManager-windows/releases/download/v1.3/TimeManager.exe)|增加定时关机，用户可选择要关机的具体时间，而不是倒计时多少分钟
|1.2.3|防止误触立即关机
|1.2.2|增加菜单栏右键【应用App】||修复因权限不足导致注册事件失败。
|1.2.1||将“任务栏显示”功能改为“隐匿”功能。主程序的界面显示改为默认不隐匿
|[1.2](https://github.com/IamWilliamWang/TimeManager-windows/releases/download/v1.2/TimeManager.exe)|增加了强大的右键功能。主界面右键包含【透明度，任务栏显示，退出】和确认键右键【注册关机事件，销毁关机事件，打开启动文件夹】
|1.1|增加了程序的主题界面。可通过主界面完成之前的操作
|1.0.2|||修复了自定义选项内只支持输入整形的问题
|1.0.1|栏内增加了自定义选项，可以通过自定义选项设置任意的关机时间；增加了退出按钮
|1.0|菜单栏基本选项构建完成，支持关机、重启、取消指令、退出
<br>

### 命令行更新日志：
|版本号|对应主版本|新功能|调整与修改|修复问题
|:-:|:-:|:-|:-|:-|
|2.1|[4.5](https://github.com/IamWilliamWang/TimeManager-windows/releases/download/v4.5/TimeManager.exe)|传入文件夹可自动补全文件名。|开机记录不再支持传文件名，默认写入缓存到程序所在文件夹。完善帮助文档
|2.0.1|[4.2](https://github.com/IamWilliamWang/TimeManager-windows/releases/download/v4.2/TimeManager.exe)|添加了缓存显示和删除功能
|2.0||添加全名称选项。所有功能支持操作指定数据库、指定缓存或切换为离线模式。|修改调用接口，休眠变为-h。合并-s和-m。完善帮助描述
|1.7|||dbFilename不再作为调用附加选项，而是单独提出来变为-db
|1.6.1|[4.0.1](https://github.com/IamWilliamWang/TimeManager-windows/releases/download/v4.0.5/TimeManager_v4.0.1.exe)||参数防缺失处理
|1.6|[4.0](https://github.com/IamWilliamWang/TimeManager-windows/releases/download/v4.0.0/TimeManager.exe)|对开机记录功能使用了缓存加速，速度提升95%
|1.5|[3.11](https://github.com/IamWilliamWang/TimeManager-windows/releases/download/v4.0.0/TimeManager_v3.11.exe)|支持主页面的休眠功能|帮助文档全面优化|修复输出功能问题
|1.4|[3.7.1](https://github.com/IamWilliamWang/TimeManager-windows/releases/download/v3.7.1/TimeManager.exe)|支持指定数据库文件功能
|1.3.1|[3.6](https://github.com/IamWilliamWang/TimeManager-windows/releases/download/v3.7.1/TimeManager_v3.6.0.exe)|||解决了提示框显示过快问题
|1.3|[3.5.1](https://github.com/IamWilliamWang/TimeManager-windows/releases/download/v3.7.1/TimeManager_v3.5.1.exe)|支持开机记录
|1.2||增添真正关机时间记录并关机的选项
|1.1||可以自定义输出以及添加了按分计时的关机选项
|1.0|[3.2](https://github.com/IamWilliamWang/TimeManager-windows/releases/download/v3.7.1/TimeManager_v3.2.0.exe)|可以进行快捷关机与快捷取消
