   M S B A M L   `   `   `  ����F  BBatchRename, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:BatchRenameBatchRename  5   	   %    BatchRename.ChangeExtensionEdit   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:BatchRename     5   	     � Title$  ChangeExtensionEdit��5   	   $	��200��6%   $	��320��62   ���#caecfc � WindowStartupLocation$ CenterScreen=�6S    � 
ResizeMode$ NoResize=�6x   .��5	      �� .+�5
   
   �� $��Change extension��6    �� TextAlignment$ Center=�6,   ���#39a2db$��25(�6X   ]� 5   
   $��30��6   $��0, 10, 0, 0q�6    .+�5      �� $��Enter extension:��6   ���#39a2db$��20(�6E   $��10, 0, 0, 0q�6S   5      ]� 5   
   $��30��6   .+�5      �� -   5         ��Name$ InputExtension��5      $	��150��6.   ���#39a2db$
��Left=�6P   $��14(�5      ���#39a2db$��Center=�69   $��10,0,0,0q�6[   �� 5      -   5         ��Name$ 
DoneButton��5      $
��Done��6)   $��50��68   $
��Left=�6C   ���#39a2db
����������#39a2db$��15,0,0,0q�5   S   5      �� 5      -   5        $ CancelButton��5      $��Cancel��6+   $��50��6<   $
��Left=�6G   ���#39a2db
����������#39a2db$��15,0,0,0q�5   S   5      5      ]� 5   
   $��0,10,0,0q�6   .+�5      �� $��Supported extension:��6   ���#39a2db$��Italic$�6I   $��14(�5      $��10,0,0,0q�6&   5       ]� 5"   
   .+�5#      �� $8��2jpg, png, doc, docx, pdf, txt, rar, zip, xlsx, ppt��6   ���#39a2db$��Italic$�6g   $��14(�5$      $��10,0,0,0q�6"   5%      5&      5'      