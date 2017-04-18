<?xml version="1.0" encoding="UTF-8"?>
<tileset name="Molten Foundary Parts" tilewidth="32" tileheight="48" tilecount="34" columns="3">
 <tile id="0">
  <image width="16" height="8" source="../../Sprites/geometry/molten_foundry/pieces/chains.png"/>
  <objectgroup draworder="index">
   <object id="1" x="3" y="8">
    <polygon points="0,0 3,0 3,-8 0,-8"/>
   </object>
   <object id="2" x="10" y="0">
    <polygon points="0,0 0,8 3,8 3,0"/>
   </object>
  </objectgroup>
 </tile>
 <tile id="1">
  <image width="16" height="8" source="../../Sprites/geometry/molten_foundry/pieces/chains_top.png"/>
  <objectgroup draworder="index">
   <object id="1" x="3" y="0">
    <polygon points="0,0 0,8 3,8 3,0"/>
   </object>
   <object id="2" x="10" y="0">
    <polygon points="0,0 0,8 3,8 3,0"/>
   </object>
  </objectgroup>
 </tile>
 <tile id="2">
  <image width="32" height="32" source="../../Sprites/geometry/molten_foundry/pieces/lava.png"/>
  <animation>
   <frame tileid="25" duration="75"/>
   <frame tileid="26" duration="75"/>
   <frame tileid="27" duration="75"/>
   <frame tileid="28" duration="75"/>
   <frame tileid="29" duration="75"/>
   <frame tileid="30" duration="75"/>
  </animation>
 </tile>
 <tile id="3">
  <image width="26" height="15" source="../../Sprites/geometry/molten_foundry/pieces/rail_long.png"/>
  <objectgroup draworder="index">
   <object id="1" x="3" y="22">
    <polygon points="0,0 0,-15 26,-15 26,0"/>
   </object>
  </objectgroup>
 </tile>
 <tile id="4">
  <image width="18" height="15" source="../../Sprites/geometry/molten_foundry/pieces/rail_short.png"/>
 </tile>
 <tile id="5">
  <image width="8" height="16" source="../../Sprites/geometry/molten_foundry/pieces/terrain_connector.png"/>
  <objectgroup draworder="index">
   <object id="1" x="0" y="0" width="8" height="12"/>
  </objectgroup>
 </tile>
 <tile id="6">
  <image width="24" height="16" source="../../Sprites/geometry/molten_foundry/pieces/terrain_end_left.png"/>
  <objectgroup draworder="index">
   <object id="2" x="24" y="0">
    <polygon points="0,0 0,16 -16,16 -21,11 -21,0"/>
   </object>
  </objectgroup>
 </tile>
 <tile id="7">
  <image width="24" height="16" source="../../Sprites/geometry/molten_foundry/pieces/terrain_end_left_with_connector.png"/>
  <objectgroup draworder="index">
   <object id="2" x="0" y="0">
    <polygon points="0,0 24,0 24,16 8,16 4,12 0,12"/>
   </object>
  </objectgroup>
 </tile>
 <tile id="8">
  <image width="24" height="16" source="../../Sprites/geometry/molten_foundry/pieces/terrain_end_right.png"/>
  <objectgroup draworder="index">
   <object id="1" x="0" y="0">
    <polygon points="0,0 21,0 21,11 16,16 0,16"/>
   </object>
  </objectgroup>
 </tile>
 <tile id="9">
  <image width="24" height="16" source="../../Sprites/geometry/molten_foundry/pieces/terrain_end_right_with_connector.png"/>
  <objectgroup draworder="index">
   <object id="2" x="0" y="16">
    <polygon points="0,0 0,-16 24,-16 24,-4 20,-4 16,0"/>
   </object>
  </objectgroup>
 </tile>
 <tile id="10">
  <image width="16" height="40" source="../../Sprites/geometry/molten_foundry/pieces/terrain_hook.png"/>
  <objectgroup draworder="index">
   <object id="1" x="0" y="0">
    <polygon points="0,0 0,21 1,21 1,24 2,24 2,25 6,25 6,28 5,28 5,30 4,30 4,31 3,31 3,36 4,36 4,37 5,37 5,38 7,38 7,39 11,39 11,38 13,38 13,36 14,36 14,33 13,33 13,34 12,34 12,35 11,35 11,36 10,36 7,36 7,35 6,35 6,32 7,32 7,31 8,31 8,30 9,30 9,29 10,29 10,25 14,25 14,24 15,24 15,21 16,21 16,0"/>
   </object>
  </objectgroup>
 </tile>
 <tile id="11">
  <image width="32" height="24" source="../../Sprites/geometry/molten_foundry/pieces/terrain_hook_holder_connector_both.png"/>
  <objectgroup draworder="index">
   <object id="1" x="0" y="20">
    <polygon points="0,0 8,0 8,1 11,4 21,4 24,1 24,0 32,0 32,-12 24,-12 24,-13 8,-13 8,-12 0,-12"/>
   </object>
  </objectgroup>
 </tile>
 <tile id="12">
  <image width="24" height="24" source="../../Sprites/geometry/molten_foundry/pieces/terrain_hook_holder_connector_left.png"/>
  <objectgroup draworder="index">
   <object id="2" x="0" y="8">
    <polygon points="0,0 8,0 8,-1 24,-1 24,13 21,16 11,16 8,13 8,12 0,12"/>
   </object>
  </objectgroup>
 </tile>
 <tile id="13">
  <image width="24" height="24" source="../../Sprites/geometry/molten_foundry/pieces/terrain_hook_holder_connector_right.png"/>
  <objectgroup draworder="index">
   <object id="2" x="24" y="8">
    <polygon points="0,0 0,12 -8,12 -8,13 -11,16 -21,16 -24,13 -24,-1 -8,-1 -8,0"/>
   </object>
  </objectgroup>
 </tile>
 <tile id="14">
  <image width="32" height="8" source="../../Sprites/geometry/molten_foundry/pieces/terrain_ladder.png"/>
  <objectgroup draworder="index">
   <object id="1" x="5" y="8">
    <polygon points="0,0 22,0 22,-8 19,-8 19,-6 3,-6 3,-8 0,-8"/>
   </object>
  </objectgroup>
 </tile>
 <tile id="15">
  <image width="32" height="16" source="../../Sprites/geometry/molten_foundry/pieces/terrain_ladder_top.png"/>
  <objectgroup draworder="index">
   <object id="1" x="0" y="16">
    <polygon points="0,0 32,0 32,-16 0,-16"/>
   </object>
  </objectgroup>
 </tile>
 <tile id="16">
  <image width="32" height="16" source="../../Sprites/geometry/molten_foundry/pieces/terrain_middle.png"/>
  <objectgroup draworder="index">
   <object id="1" x="0" y="16">
    <polygon points="0,0 32,0 32,-16 0,-16"/>
   </object>
  </objectgroup>
 </tile>
 <tile id="17">
  <image width="32" height="40" source="../../Sprites/geometry/molten_foundry/pieces/terrain_support_bg.png"/>
  <objectgroup draworder="index">
   <object id="1" x="0" y="0">
    <polygon points="0,0 0,3 4,7 4,33 0,37 0,40 26,40 26,37 22,33 22,7 26,3 26,0"/>
   </object>
  </objectgroup>
 </tile>
 <tile id="18">
  <image width="32" height="40" source="../../Sprites/geometry/molten_foundry/pieces/terrain_support_bg_lava.png"/>
  <objectgroup draworder="index">
   <object id="2" x="0" y="0">
    <polygon points="0,0 0,3 4,7 4,33 0,37 0,40 26,40 26,37 22,33 22,7 26,3 26,0"/>
   </object>
  </objectgroup>
 </tile>
 <tile id="19">
  <image width="32" height="40" source="../../Sprites/geometry/molten_foundry/pieces/terrain_support_bg_lava_top.png"/>
  <objectgroup draworder="index">
   <object id="1" x="0" y="0">
    <polygon points="0,0 0,3 4,7 4,33 0,37 0,40 26,40 26,37 22,33 22,7 26,3 26,0"/>
   </object>
  </objectgroup>
 </tile>
 <tile id="20">
  <image width="32" height="40" source="../../Sprites/geometry/molten_foundry/pieces/terrain_support_fg.png"/>
  <objectgroup draworder="index">
   <object id="1" x="3" y="0">
    <polygon points="0,0 0,3 4,7 4,33 0,37 0,40 26,40 26,37 22,33 22,7 26,3 26,0"/>
   </object>
  </objectgroup>
 </tile>
 <tile id="21">
  <image width="32" height="40" source="../../Sprites/geometry/molten_foundry/pieces/terrain_support_fg_lava.png"/>
  <objectgroup draworder="index">
   <object id="1" x="3" y="0">
    <polygon points="0,0 0,3 4,7 4,33 0,37 0,40 26,40 26,37 22,33 22,7 26,3 26,0"/>
   </object>
  </objectgroup>
 </tile>
 <tile id="22">
  <image width="32" height="40" source="../../Sprites/geometry/molten_foundry/pieces/terrain_support_fg_lava_top.png"/>
  <objectgroup draworder="index">
   <object id="1" x="3" y="0">
    <polygon points="0,0 0,3 4,7 4,33 0,37 0,40 26,40 26,37 22,33 22,7 26,3 26,0"/>
   </object>
  </objectgroup>
 </tile>
 <tile id="23">
  <image width="32" height="48" source="../../Sprites/geometry/molten_foundry/pieces/terrain_support_top.png"/>
  <objectgroup draworder="index">
   <object id="1" x="0" y="0">
    <polygon points="0,0 32,0 32,16 25,16 25,41 29,45 29,48 3,48 3,45 7,41 7,16 0,16"/>
   </object>
  </objectgroup>
 </tile>
 <tile id="25">
  <image width="32" height="32" source="../../Sprites/geometry/molten_foundry/pieces/lava_anim/lava_f0.png"/>
 </tile>
 <tile id="26">
  <image width="32" height="32" source="../../Sprites/geometry/molten_foundry/pieces/lava_anim/lava_f1.png"/>
 </tile>
 <tile id="27">
  <image width="32" height="32" source="../../Sprites/geometry/molten_foundry/pieces/lava_anim/lava_f2.png"/>
 </tile>
 <tile id="28">
  <image width="32" height="32" source="../../Sprites/geometry/molten_foundry/pieces/lava_anim/lava_f3.png"/>
 </tile>
 <tile id="29">
  <image width="32" height="32" source="../../Sprites/geometry/molten_foundry/pieces/lava_anim/lava_f4.png"/>
 </tile>
 <tile id="30">
  <image width="32" height="32" source="../../Sprites/geometry/molten_foundry/pieces/lava_anim/lava_f5.png"/>
 </tile>
 <tile id="35">
  <image width="24" height="16" source="../../Sprites/geometry/molten_foundry/pieces/bg_pillars_side_1.png"/>
 </tile>
 <tile id="36">
  <image width="24" height="48" source="../../Sprites/geometry/molten_foundry/pieces/bg_pillars_side_2.png"/>
 </tile>
 <tile id="37">
  <image width="24" height="16" source="../../Sprites/geometry/molten_foundry/pieces/bg_pillars_side_3.png"/>
 </tile>
 <tile id="38">
  <image width="24" height="48" source="../../Sprites/geometry/molten_foundry/pieces/bg_pillars_side_4.png"/>
 </tile>
</tileset>
