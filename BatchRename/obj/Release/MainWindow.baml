   M S B A M L   `   `   `  ���� F  BBatchRename, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:BatchRenameBatchRename  M IFluent, Version=9.0.0.0, Culture=neutral, PublicKeyToken=3e436e32a8c5546f/$clr-namespace:Fluent;assembly=FluentFluent     BatchRename.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     +localclr-namespace:BatchRename     7Fluent$clr-namespace:Fluent;assembly=Fluent     -     � Title$  Batch Rename��$	��600��$
��1200�� � 
ResizeMode$ NoResize=�.���  � ColumnDefinitions �� $	��370 ��� .+��� $��0��.+��� $��30��$��
15,15,13,0q� �� Orientation$ 
Horizontal=�.+��� -    ��Name$ newWorkButton��$��80��$��30��
������ ���#39a2db���#39a2db$��0,0,0,0q�.���� $ 
Horizontal=�.+��� $��/Images/new.png��$��20��$��20���� $	��New��$��5,0,0,0q��� -   $ openWorkButton��$��80��$��30��
������ ���#39a2db���#39a2db$��7,0,0,0q�.���� $ 
Horizontal=�.+��� $��/Images/open.png��$��20��$��20���� $
��Open��$��5,0,0,0q��� -   $ saveWorkButton��$��80��$��30��
������ ���#39a2db���#39a2db$��7,0,0,0q�.���� $ 
Horizontal=�.+��� $��/Images/save.png��$��20��$��20���� $
��Save��$��5,0,0,0q��� -   $ saveAsWorkButton��$��80��$��30��
������ ���#39a2db���#39a2db$��7,0,0,0q�.���� $ 
Horizontal=�.+��� $��/Images/save_as.png��$��20��$��20���� $��Save as��$��5,0,0,0q��� $��30��$��
15,15,13,0q�$ 
Horizontal=�.+��� -    ��Name$ presetLabel��$��Preset: �����#39a2db$��18(��� -    ��Name$ presetCombobox��$	��165��$��20,0,0,0q��� -   $ deletePresetButton��$��80��$��30��
������ ���#39a2db���#39a2db$��7,0,0,0q�.���� $ 
Horizontal=�.+��� $��/Images/delete.png��$��20��$��20���� $��Delete��$��5,0,0,0q��� $��30��$��
15,15,13,0q�$ 
Horizontal=�.+��� -	   $ 	ruleLabel��$��Rule: �����#39a2db$��18(��� -
   $ ruleCombobox��$	��165��$��34,0,0,0q����#39a2db$��14(��� -   $ addRuleButton��$��80��$��30��
������ ���#39a2db���#39a2db$��7,0,0,0q�.���� $ 
Horizontal=�.+��� $��/Images/add.png��$��20��$��20���� $	��Add��$��5,0,0,0q��� $��30��$��
15,15,13,0q�$ 
Horizontal=�.+��� -   $ presetNameLabel��$��Preset name: �����#39a2db$��18(��� -    ��Name$ presetNameTextbox��$	��225�����#39a2db$��Center=�$��15(����#39a2db�� -    ��Name$ rulesListView�����#caecfc$	��332��$��
15,15,13,0q����#39a2db	 � 	Resources	    rulesListViewContextMenu
 m�Name m� Icon��%�   &	        �� .@�m� $
 EditRule��-   $
��Edit�� �� $��/Images/edit.png��m� $
 
RemoveRule��-   $��Remove�� �� $��/Images/delete.png������  �� 
TargetType" ��.���  �� Property �@�ContextMenu �� Value# [  ���� .R��� ���#39a2db$��14(����� *
ruleName+�� $��30��$��
15,15,13,0q�$ 
Horizontal=�.+��� -   $ savePresetButton��$��95��$��30��$��49,0,0,0q�
������ ���#39a2db���#39a2db.���� $ 
Horizontal=�.+��� $��/Images/save.png��$��20��$��20���� $��Save preset��$��5,0,0,0q��� -   $ saveAsPresetButton��$	��115��$��30��$��39,0,0,0q�
������ ���#39a2db���#39a2db.���� $ 
Horizontal=�.+��� $��/Images/save_as.png��$��20��$��20���� $��Save preset as��$��5,0,0,0q��� $��1��.+��� $��30��$��
15,15,13,0q�$ 
Horizontal=�.+��� -   $ startBatchButton��$	��120��$��30��
������ ���#39a2db���#39a2db$��4,0,0,0q�.���� $ 
Horizontal=�.+��� $��/Images/start.png��$��20��$��20���� $��Start batch��$��5,0,0,0q��� -   $ previewButton��$	��120��$��30��
������ ���#39a2db���#39a2db$��15,0,0,0q�.���� $ 
Horizontal=�.+��� $��/Images/view.png��$��20��$��20���� $��Preview name��$��5,0,0,0q�]� ��. .+�  Fluent.Ribbon  $	��Top=�$��75��$��	15,15,0,0q�   Tabs.   Fluent.RibbonTabItem  -      Header$
 File�����#39a2db$��30��   Groups.   Fluent.RibbonGroupBox     IsSeparatorVisible .  .@��� -   $ addFileButton��$	��120��$��30��
������ ���#39a2db���#39a2db$��0,13,0,0q�$	��Top=�.���� $ 
Horizontal=�.+��� $��/Images/add.png��$��20��$��20���� $��Add new file��$��5,0,0,0q��� -   $ removeFileButton��$	��120��$��30��
������ ���#39a2db���#39a2db$��	15,13,0,0q�$	��Top=�.���� $ 
Horizontal=�.+��� $��/Images/delete.png��$��20��$��20���� $��Remove file��$��5,0,0,0q��� -   $ selectAllFiles��$	��120��$��30��
������ ���#39a2db���#39a2db$��	15,13,0,0q�$	��Top=�.���� $ 
Horizontal=�.+��� $��/Images/select.png��$��20��$��20���� $��Select all files��$��5,0,0,0q�  -   $ Folder�����#39a2db$��30��.    .  .@��� -   $ addFolderButton��$	��120��$��30��
������ ���#39a2db���#39a2db$��0,13,0,0q�$	��Top=�.���� $ 
Horizontal=�.+��� $��/Images/add.png��$��20��$��20���� $��Add new folder��$��5,0,0,0q��� -   $ removeFolderButton��$	��120��$��30��
������ ���#39a2db���#39a2db$��	15,13,0,0q�$	��Top=�.���� $ 
Horizontal=�.+��� $��/Images/delete.png��$��20��$��20���� $��Remove folder��$��5,0,0,0q��� -   $ selectAllFolders��$	��120��$��30��
������ ���#39a2db���#39a2db$��	15,13,0,0q�$	��Top=�.���� $ 
Horizontal=�.+��� $��/Images/select.png��$��20��$��20���� $��Select all folders��$��5,0,0,0q��� $	��377��.+��� -   $ fileListView�����#caecfc���#39a2db$	��377��$	��800��$��15,15,13,15q� �� View �� ! �� ColumnHeaderContainerStyle �� " ��.���  � {� .  .P��� $��	File name�� �� Width$	 180�� �� CellTemplate �� .R��� ���#39a2db���� *name+�� $��	Extension��$	 120�� �� .R��� ���#39a2db���� *	extension+�� $��New file name��$	 180�� �� .R��� ���#39a2db���� *	newName+�� $
��Path��$	 290�� �� .R��� ���#39a2db���� *path+�� $ 40�� �� .R���  d� 	IsChecked ��  �� Path$ 
isSelected� �� Mode$ TwoWay=��� -   $ folderListView�����#caecfc���#39a2db$	��377��$	��800��$��
15,15,13,0q� ��  �� " ��.���  � {� .  .P��� $��Folder name��$	 180�� �� .R��� ���#39a2db���� *name+�� $��	Extension��$	 120�� �� .R��� ���#39a2db���� *	extension+�� $��New folder name��$	 180�� �� .R��� ���#39a2db���� *	newName+�� $
��Path��$	 290�� �� .R��� ���#39a2db���� *path+�� $ 40�� �� .R���  �� $ 
isSelected�$ TwoWay=�